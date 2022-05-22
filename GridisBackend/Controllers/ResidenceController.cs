using GridisBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenceController : ControllerBase
    {

        private readonly PowerManagementOLTPContext _context;


        public ResidenceController(PowerManagementOLTPContext context)
        {
            _context = context;
        }

        // GET: api/<ResidenceController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Residence>>> Get()
        {
            return await _context.Residences.Include(residence=>residence.Resident).ToListAsync();
        }

        // GET api/<ResidenceController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Residence>> Get(int id)
        {
            var residence = await _context.Residences.FindAsync(id);

            if (residence == null)
            {
                return NotFound();
            }

            return residence;
        }
    }
}
