using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Person;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public PeopleController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person_GET_POST_DTO>>> GetPeople()
        {
            if (_context.People == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Person_GET_POST_DTO>>(_context.People.ToList());

            return Ok(data);
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person_GET_POST_DTO>> GetPerson(int id)
        {
          if (_context.People == null)
          {
              return NotFound();
          }
            var person = await _context.People.FirstOrDefaultAsync(i => i.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return _mapper.Map<Person, Person_GET_POST_DTO>(person);
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person_GET_POST_DTO personDTO)
        {
            var person = await _context.People.SingleOrDefaultAsync(t => t.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            _mapper.Map<Person_GET_POST_DTO, Person>(personDTO, person);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person_GET_POST_DTO personDTO)
        {
            if (_context.People == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Person'  is null.");
            }

            var person = _mapper.Map<Person>(personDTO);
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (_context.People == null)
            {
                return NotFound();
            }
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return (_context.People?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
