using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Service;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;


        public ServicesController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service_GET_POST_DTO>>> GetServices()
        {
            if (_context.Districts == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Service_GET_POST_DTO>>(_context.Services.ToList());

            return Ok(data);
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service_GET_POST_DTO>> GetService(int id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FirstOrDefaultAsync(i => i.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return _mapper.Map<Service, Service_GET_POST_DTO>(service);
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service_GET_POST_DTO serviceDTO)
        {
            var service = await _context.Services.SingleOrDefaultAsync(t => t.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            _mapper.Map<Service_GET_POST_DTO, Service>(serviceDTO, service);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service_GET_POST_DTO serviceDTO)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Services'  is null.");
            }

            var service = _mapper.Map<Service>(serviceDTO);
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return Ok(service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
