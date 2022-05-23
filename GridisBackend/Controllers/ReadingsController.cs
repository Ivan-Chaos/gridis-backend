using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Readings;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public ReadingsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Readings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reading_GET_DTO>>> GetReadings()
        {
            if (_context.Readings == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Reading_GET_DTO>>(_context.Readings.Include(r=>r.InstalledMeter).ThenInclude(im=>im.Model).ThenInclude(mm=>mm.Manufacturer).ToList());

            return Ok(data);
        }

        // GET: api/Readings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reading_GET_DTO>> GetReading(int id)
        {
            if (_context.Readings == null)
            {
                return NotFound();
            }
            var reading = await _context.Readings.Include(r => r.InstalledMeter).ThenInclude(im => im.Model).ThenInclude(mm => mm.Manufacturer).FirstOrDefaultAsync(i => i.Id == id);

            if (reading == null)
            {
                return NotFound();
            }

            return _mapper.Map<Reading, Reading_GET_DTO>(reading);
        }

        // PUT: api/Readings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReading(int id, Reading_POST_DTO readingDTO)
        {
            var reading = await _context.Readings.SingleOrDefaultAsync(t => t.Id == id);

            if (reading == null)
            {
                return NotFound();
            }

            _mapper.Map<Reading_POST_DTO, Reading>(readingDTO, reading);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReadingExists(id))
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

        // POST: api/Readings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reading>> PostReading(Reading_POST_DTO readingDTO)
        {
            if (_context.Readings == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Readings' is null.");
            }
            var reading = _mapper.Map<Reading>(readingDTO);
            _context.Readings.Add(reading);
            await _context.SaveChangesAsync();

            return Ok(reading);
        }

        // DELETE: api/Readings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReading(int id)
        {
            if (_context.Readings == null)
            {
                return NotFound();
            }
            var reading = await _context.Readings.FindAsync(id);
            if (reading == null)
            {
                return NotFound();
            }

            _context.Readings.Remove(reading);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReadingExists(int id)
        {
            return (_context.Readings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
