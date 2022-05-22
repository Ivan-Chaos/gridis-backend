using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.District;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public DistrictsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Districts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<District_GET_DTO>>> GetDistricts()
        {
          if (_context.Districts == null)
          {
              return NotFound();
          }
          var data = _mapper.Map<List<District_GET_DTO>>(_context.Districts.Include(d => d.City).ToList());

          return Ok(data);
        }

        // GET: api/Districts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<District_GET_DTO>> GetDistrict(int id)
        {
          if (_context.Districts == null)
          {
              return NotFound();
          }
            var district = await _context.Districts.Include(d => d.City).FirstOrDefaultAsync(i => i.Id == id);

            if (district == null)
            {
                return NotFound();
            }

            return _mapper.Map<District, District_GET_DTO>(district);
        }

        // PUT: api/Districts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrict(int id, District_POST_DTO districtDTO)
        {
            var district = await _context.Districts.SingleOrDefaultAsync(t => t.Id == id);

            if (district == null)
            {
                return NotFound();
            }

            _mapper.Map<District_POST_DTO, District>(districtDTO, district);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictExists(id))
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

        // POST: api/Districts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<District>> PostDistrict(District_POST_DTO districtDTO)
        {
          if (_context.Districts == null)
          {
              return Problem("Entity set 'PowerManagementOLTPContext.Districts'  is null.");
          }

            var district = _mapper.Map<District>(districtDTO);
            _context.Districts.Add(district);
            await _context.SaveChangesAsync();

            return Ok(district);
        }

        // DELETE: api/Districts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            if (_context.Districts == null)
            {
                return NotFound();
            }
            var district = await _context.Districts.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DistrictExists(int id)
        {
            return (_context.Districts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
