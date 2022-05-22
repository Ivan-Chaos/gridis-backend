using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using GridisBackend.DTOs.Address;
using AutoMapper;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public AddressesController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address_GET_DTO>>> GetAddresses()
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Address_GET_DTO>>(_context.Addresses.Include(a => a.Street).ThenInclude(s=>s.District).ThenInclude(d=>d.City).ToList());

            return Ok(data);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address_GET_DTO>> GetAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            var address = await _context.Addresses.Include(a => a.Street).ThenInclude(s => s.District).ThenInclude(d => d.City).FirstOrDefaultAsync(i => i.Id == id);

            if (address == null)
            {
                return NotFound();
            }

            return _mapper.Map<Address, Address_GET_DTO>(address);
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address_POST_DTO addressDTO)
        {
            var address = await _context.Addresses.SingleOrDefaultAsync(t => t.Id == id);

            if (address == null)
            {
                return NotFound();
            }

            _mapper.Map<Address_POST_DTO, Address>(addressDTO, address);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address_POST_DTO addressDTO)
        {
            if (_context.Addresses == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Addresses'  is null.");
            }

            var address = _mapper.Map<Address>(addressDTO);
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return Ok(address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Addresses == null)
            {
                return NotFound();
            }
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Addresses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
