using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.OperatorReadings;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorReadingsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public OperatorReadingsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OperatorReadings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperatorReading_GET_DTO>>> GetOperatorReadings()
        {
            if (_context.OperatorReadings == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<OperatorReading_GET_DTO>>(_context.OperatorReadings.Include(or => or.Operator).ThenInclude(o=>o.Person).Include(or=>or.Readings).ThenInclude(r=>r.InstalledMeter).ToList());

            return Ok(data);
        }

        // GET: api/OperatorReadings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperatorReading_GET_DTO>> GetOperatorReading(int id)
        {
            if (_context.OperatorReadings == null)
            {
                return NotFound();
            }
            var operatorReadings = await _context.OperatorReadings.Include(or => or.Operator).ThenInclude(o => o.Person).Include(or => or.Readings).ThenInclude(r => r.InstalledMeter).FirstOrDefaultAsync(i => i.Id == id);

            if (operatorReadings == null)
            {
                return NotFound();
            }

            return _mapper.Map<OperatorReading, OperatorReading_GET_DTO>(operatorReadings);
        }

        // PUT: api/OperatorReadings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperatorReading(int id, OperatorReading_POST_DTO operatorReadingDTO)
        {
            var operatorReading = await _context.OperatorReadings.SingleOrDefaultAsync(t => t.Id == id);

            if (operatorReading == null)
            {
                return NotFound();
            }

            _mapper.Map<OperatorReading_POST_DTO, OperatorReading>(operatorReadingDTO, operatorReading);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperatorReadingExists(id))
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

        // POST: api/OperatorReadings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OperatorReading>> PostOperatorReading(OperatorReading_POST_DTO operatorReadingDTO)
        {
            if (_context.OperatorReadings == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.OperatorReadings'  is null.");
            }

            var operatorReading = _mapper.Map<OperatorReading>(operatorReadingDTO);
            _context.OperatorReadings.Add(operatorReading);
            await _context.SaveChangesAsync();

            return Ok(operatorReading);
        }

        // DELETE: api/OperatorReadings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperatorReading(int id)
        {
            if (_context.OperatorReadings == null)
            {
                return NotFound();
            }
            var operatorReading = await _context.OperatorReadings.FindAsync(id);
            if (operatorReading == null)
            {
                return NotFound();
            }

            _context.OperatorReadings.Remove(operatorReading);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperatorReadingExists(int id)
        {
            return (_context.OperatorReadings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
