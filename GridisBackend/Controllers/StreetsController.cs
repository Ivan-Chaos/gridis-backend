using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Street;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public StreetsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Streets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street_GET_DTO>>> GetStreets()
        {
            if (_context.Streets == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Street_GET_DTO>>(_context.Streets.Include(s => s.District).ThenInclude(d=>d.City).ToList());

            return Ok(data);
        }

        // GET: api/Streets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Street_GET_DTO>> GetStreet(int id)
        {
            if (_context.Streets == null)
            {
                return NotFound();
            }
            var street = await _context.Streets.Include(s => s.District).ThenInclude(d => d.City).FirstOrDefaultAsync(i => i.Id == id);

            if (street == null)
            {
                return NotFound();
            }

            return _mapper.Map<Street, Street_GET_DTO>(street);
        }

        // PUT: api/Streets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStreet(int id, Street_POST_DTO streetDTO)
        {
            var street = await _context.Streets.SingleOrDefaultAsync(t => t.Id == id);

            if (street == null)
            {
                return NotFound();
            }

            _mapper.Map<Street_POST_DTO, Street>(streetDTO, street);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreetExists(id))
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

        // POST: api/Streets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Street>> PostStreet(Street_POST_DTO streetDTO)
        {
            if (_context.Streets == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Streets'  is null.");
            }

            var street = _mapper.Map<Street>(streetDTO);
            _context.Streets.Add(street);
            await _context.SaveChangesAsync();

            return Ok(street);
        }

        // DELETE: api/Streets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStreet(int id)
        {
            if (_context.Streets == null)
            {
                return NotFound();
            }
            var street = await _context.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }

            _context.Streets.Remove(street);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StreetExists(int id)
        {
            return (_context.Streets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
