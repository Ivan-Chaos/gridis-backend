using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Engineer;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineersController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public EngineersController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Engineers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Engineer_GET_DTO>>> GetEngineers()
        {
            if (_context.Engineers == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Engineer_GET_DTO>>(_context.Engineers.Include(e => e.Person).Include(e=>e.District).ThenInclude(d=>d.City).Include(e=>e.ProvidedServices).ToList());

            return Ok(data);
        }

        // GET: api/Engineers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Engineer_GET_DTO>> GetEngineer(int id)
        {
            if (_context.Engineers == null)
            {
                return NotFound();
            }
            var engineer = await _context.Engineers.Include(e => e.Person)
                .Include(e => e.District)
                .ThenInclude(d => d.City)
                .Include(e => e.ProvidedServices)
                .ThenInclude(ps => ps.ServiceRequest)
                .ThenInclude(sr => sr.Residence)
                .ThenInclude(r => r.Address)
                .ThenInclude(r => r.Street)
                .ThenInclude(s => s.District)
                .ThenInclude(d => d.City)
                .Include(e => e.ProvidedServices)
                .ThenInclude(ps => ps.ServiceRequest)
                .ThenInclude(sr => sr.Service)
                .Include(e => e.ProvidedServices)
                .ThenInclude(ps => ps.ServiceRequest)
                .ThenInclude(sr => sr.MeterModel)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (engineer == null)
            {
                return NotFound();
            }

            return _mapper.Map<Engineer, Engineer_GET_DTO>(engineer);
        }

        // PUT: api/Engineers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEngineer(int id, Engineer_POST_DTO engineerDTO)
        {
            var engineer = await _context.Engineers.SingleOrDefaultAsync(t => t.Id == id);

            if (engineer == null)
            {
                return NotFound();
            }

            _mapper.Map<Engineer_POST_DTO, Engineer>(engineerDTO, engineer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineerExists(id))
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

        // POST: api/Engineers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Engineer>> PostEngineer(Engineer_POST_DTO engineerDTO)
        {
            if (_context.Engineers == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Engineers'  is null.");
            }

            var engineer = _mapper.Map<Engineer>(engineerDTO);
            _context.Engineers.Add(engineer);
            await _context.SaveChangesAsync();

            return Ok(engineer);
        }

        // DELETE: api/Engineers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngineer(int id)
        {
            if (_context.Engineers == null)
            {
                return NotFound();
            }
            var engineer = await _context.Engineers.FindAsync(id);
            if (engineer == null)
            {
                return NotFound();
            }

            _context.Engineers.Remove(engineer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EngineerExists(int id)
        {
            return (_context.Engineers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
