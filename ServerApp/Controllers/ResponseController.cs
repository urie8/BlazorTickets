using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Database;
using Shared.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        AppDbContext _context;

        [HttpGet]
        public ActionResult<List<ResponseModel>> Get()
        {
            return Ok(_context.Responses.Include(r => r.Ticket).ToList());
        }

        [HttpPost]
        public ActionResult<ResponseModel> Post(ResponseModel response)
        {
            if (response != null)
            {
                _context.Responses.Add(response);
                return Ok();
            }

            return BadRequest();
        }

    }
}
