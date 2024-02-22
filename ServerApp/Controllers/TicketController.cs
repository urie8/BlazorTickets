using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Database;
using Shared.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        public TicketController(AppDbContext context)
        {
            _context = context;
        }
        AppDbContext _context;

        [HttpGet]
        public ActionResult<List<TicketModel>> Get()
        {
            return Ok(_context.Tickets.Include(t => t.TicketTags).ThenInclude(t => t.Tag).ToList());
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
                        existingTag.Name = tag;
                        _context.Tags.Add(existingTag);
                    }

                    TicketTag newTicketTag = new()
                    {
                        Ticket = ticket,
                        Tag = existingTag
                    };

                    _context.TicketTags.Add(newTicketTag);

                }

                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
    }
}
