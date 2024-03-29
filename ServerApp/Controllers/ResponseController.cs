﻿using Microsoft.AspNetCore.Mvc;
using ServerApp.Database;
using Shared.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        AppDbContext _context;

        public ResponseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{ticketId}")]
        public ActionResult<List<ResponseModel>> Get(int ticketId)
        {
            return Ok(_context.Responses.Where(r => r.TicketId == ticketId).ToList());
        }

        [HttpPost]
        public ActionResult<ResponseModel> Post(ResponseModel response)
        {
            if (response != null)
            {
                _context.Responses.Add(response);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

    }
}
