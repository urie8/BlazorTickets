﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Database;
using Shared.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        AppDbContext _context;
        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<TicketModel>> Get()
        {
            return Ok(_context.Tickets.Include(t => t.TicketTags).ThenInclude(t => t.Tag).ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<TicketModel> Get(int id)
        {
            return Ok(_context.Tickets.Include(t => t.TicketTags).ThenInclude(t => t.Tag).FirstOrDefault(t => t.Id == id));
        }


        [HttpPost]
        public ActionResult<TicketApiModel> Post(TicketApiModel apiTicket)
        {
            if (apiTicket != null)
            {
                TicketModel ticket = new()
                {
                    Title = apiTicket.Title,
                    Description = apiTicket.Description,
                    SubmittedBy = apiTicket.SubmittedBy,
                    IsResolved = apiTicket.IsResolved,
                };

                _context.Tickets.Add(ticket);

                foreach (var tag in apiTicket.Tags)
                {
                    TagModel existingTag = _context.Tags.FirstOrDefault(t => t.Name == tag);

                    if (existingTag == null)
                    {
                        existingTag = new();
                        existingTag.Name = tag;
                        _context.Tags.Add(existingTag);
                    }

                    TicketTag newTicketTag = new()
                    {
                        Ticket = ticket,
                        Tag = existingTag
                    };



                }

                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
    }
}
