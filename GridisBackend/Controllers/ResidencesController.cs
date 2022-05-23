using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Residence;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidencesController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public ResidencesController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Residences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Residence_GET_DTO>>> GetResidences()
        {
            if (_context.Residences == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Residence_GET_DTO>>(_context.Residences.Include(r => r.Resident).Include(r=>r.InstalledMeter).Include(r=>r.Bills).ThenInclude(b=>b.Readings).Include(r=>r.Address).ThenInclude(a=>a.Street).ThenInclude(s=>s.District).ThenInclude(d=>d.City).ToList());

            return Ok(data);
        }

        // GET: api/Residences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Residence_GET_DTO>> GetResidence(int id)
        {
            if (_context.Residences == null)
            {
                return NotFound();
            }
            var residence = await _context.Residences.Include(r => r.Resident).Include(r => r.InstalledMeter).Include(r => r.Bills).ThenInclude(b => b.Readings).Include(r => r.Address).ThenInclude(a => a.Street).ThenInclude(s => s.District).ThenInclude(d => d.City).FirstOrDefaultAsync(i => i.Id == id);

            if (residence == null)
            {
                return NotFound();
            }

            return _mapper.Map<Residence, Residence_GET_DTO>(residence);
        }

        // PUT: api/Residences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResidence(int id, Residence_POST_DTO residenceDTO)
        {
            var residence = await _context.Residences.SingleOrDefaultAsync(t => t.Id == id);

            if (residence == null)
            {
                return NotFound();
            }

            _mapper.Map<Residence_POST_DTO, Residence>(residenceDTO, residence);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidenceExists(id))
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

        // POST: api/Residences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Residence>> PostResidence(Residence_POST_DTO residenceDTO)
        {
            if (_context.Residences == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Residences'  is null.");
            }

            var residence = _mapper.Map<Residence>(residenceDTO);
            _context.Residences.Add(residence);
            await _context.SaveChangesAsync();

            return Ok(residence);
        }

        // DELETE: api/Residences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResidence(int id)
        {
            if (_context.Residences == null)
            {
                return NotFound();
            }
            var residence = await _context.Residences.FindAsync(id);
            if (residence == null)
            {
                return NotFound();
            }

            _context.Residences.Remove(residence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResidenceExists(int id)
        {
            return (_context.Residences?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
