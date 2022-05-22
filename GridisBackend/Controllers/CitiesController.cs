using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using GridisBackend.DTOs;
using AutoMapper;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public CitiesController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City_GET_POST_DTO>>> GetCities()
        {
          if (_context.Cities == null)
          {
              return NotFound();
          }
            var data = await _context.Cities.Select(t => _mapper.Map<City_GET_POST_DTO>(t)).ToListAsync();
            return Ok(data);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City_GET_POST_DTO>> GetCity(int id)
        {
          if (_context.Cities == null)
          {
              return NotFound();
          }
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return _mapper.Map<City, City_GET_POST_DTO>(city);
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City_GET_POST_DTO cityDTO)
        {
            
            var city = await _context.Cities.SingleOrDefaultAsync(t => t.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            _mapper.Map<City_GET_POST_DTO, City>(cityDTO, city);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City_GET_POST_DTO cityDTO)
        {
          if (_context.Cities == null)
          {
              return Problem("Entity set 'PowerManagementOLTPContext.Cities'  is null.");
          }

          var city = _mapper.Map<City>(cityDTO);

            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return Ok(city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return (_context.Cities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
