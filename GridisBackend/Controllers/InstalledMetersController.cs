using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.InstalledMeter;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstalledMetersController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public InstalledMetersController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/InstalledMeters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstalledMeter_GET_DTO>>> GetInstalledMeters()
        {
            if (_context.InstalledMeters == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<InstalledMeter_GET_DTO>>(_context.InstalledMeters.Include(im => im.Model).ThenInclude(mm=>mm.Manufacturer).ToList());

            return Ok(data);
        }

        // GET: api/InstalledMeters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstalledMeter_GET_DTO>> GetInstalledMeter(int id)
        {
            if (_context.InstalledMeters == null)
            {
                return NotFound();
            }
            var installedMeter = await _context.InstalledMeters.Include(im => im.Model).ThenInclude(mm => mm.Manufacturer).FirstOrDefaultAsync(i => i.Id == id);

            if (installedMeter == null)
            {
                return NotFound();
            }

            return _mapper.Map<InstalledMeter, InstalledMeter_GET_DTO>(installedMeter);
        }

        // PUT: api/InstalledMeters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstalledMeter(int id, InstalledMeter_POST_DTO installedMeterDTO)
        {
            var installedMeter = await _context.InstalledMeters.SingleOrDefaultAsync(t => t.Id == id);

            if (installedMeter == null)
            {
                return NotFound();
            }

            _mapper.Map<InstalledMeter_POST_DTO, InstalledMeter>(installedMeterDTO, installedMeter);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstalledMeterExists(id))
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

        // POST: api/InstalledMeters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InstalledMeter>> PostInstalledMeter(InstalledMeter_POST_DTO installedMeterDTO)
        {
            if (_context.InstalledMeters == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.InstalledMeters'  is null.");
            }

            var installedMeter = _mapper.Map<InstalledMeter>(installedMeterDTO);
            _context.InstalledMeters.Add(installedMeter);
            await _context.SaveChangesAsync();

            return Ok(installedMeter);
        }

        // DELETE: api/InstalledMeters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstalledMeter(int id)
        {
            if (_context.InstalledMeters == null)
            {
                return NotFound();
            }
            var installedMeter = await _context.InstalledMeters.FindAsync(id);
            if (installedMeter == null)
            {
                return NotFound();
            }

            _context.InstalledMeters.Remove(installedMeter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstalledMeterExists(int id)
        {
            return (_context.InstalledMeters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
