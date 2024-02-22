using Microsoft.AspNetCore.Http;
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
        AppDbContext _context;

        [HttpGet]
        public ActionResult<List<TicketModel>> Get()
        {
            return Ok(_context.Tickets.Include(t => t.TicketTags).ThenInclude(t => t.Tag).ToList());
        }


        [HttpPost]
        public ActionResult<TicketModel> Post(TicketModel ticket)
        {
            if(ticket != null)
            {
                _context.Tickets.Add(ticket);
                return Ok();
            }

            return BadRequest();
        } 
    }
}
