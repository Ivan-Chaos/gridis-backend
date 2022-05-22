using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Manufacturer;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public ManufacturersController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Manufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturer_GET_POST_DTO>>> GetManufacturers()
        {
            if (_context.Manufacturers == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Manufacturer_GET_POST_DTO>>(_context.Manufacturers.ToList());

            return Ok(data); ;
        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer_GET_POST_DTO>> GetManufacturer(int id)
        {
          if (_context.Manufacturers == null)
          {
              return NotFound();
          }
            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync(i => i.Id == id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return _mapper.Map<Manufacturer, Manufacturer_GET_POST_DTO>(manufacturer);
        }

        // PUT: api/Manufacturers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturer(int id, Manufacturer_GET_POST_DTO manufacturerDTO)
        {
            var manufacturer = await _context.Manufacturers.SingleOrDefaultAsync(t => t.Id == id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            _mapper.Map<Manufacturer_GET_POST_DTO, Manufacturer>(manufacturerDTO, manufacturer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
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

        // POST: api/Manufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> PostManufacturer(Manufacturer_GET_POST_DTO manufacturerDTO)
        {
            if (_context.Manufacturers == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Manufacturers'  is null.");
            }

            var manufacturer = _mapper.Map<Manufacturer>(manufacturerDTO);
            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();

            return Ok(manufacturer);
        }

        // DELETE: api/Manufacturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            if (_context.Manufacturers == null)
            {
                return NotFound();
            }
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManufacturerExists(int id)
        {
            return (_context.Manufacturers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
