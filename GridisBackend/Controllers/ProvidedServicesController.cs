using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.ProvidedService;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidedServicesController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public ProvidedServicesController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ProvidedServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvidedService_GET_DTO>>> GetProvidedServices()
        {
            if (_context.ProvidedServices == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<ProvidedService_GET_DTO>>(_context.ProvidedServices.Include(ps => ps.ServiceRequest).ToList());

            return Ok(data);
        }

        // GET: api/ProvidedServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProvidedService_GET_DTO>> GetProvidedService(int id)
        {
            if (_context.ProvidedServices == null)
            {
                return NotFound();
            }
            var providedService = await _context.ProvidedServices.Include(ps => ps.ServiceRequest).FirstOrDefaultAsync(i => i.Id == id);

            if (providedService == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProvidedService, ProvidedService_GET_DTO>(providedService);
        }

        // PUT: api/ProvidedServices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvidedService(int id, ProvidedService_POST_DTO providedServiceDTO)
        {
            var providedService = await _context.ProvidedServices.SingleOrDefaultAsync(t => t.Id == id);

            if (providedService == null)
            {
                return NotFound();
            }

            _mapper.Map<ProvidedService_POST_DTO, ProvidedService>(providedServiceDTO, providedService);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvidedServiceExists(id))
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

        // POST: api/ProvidedServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProvidedService>> PostProvidedService(ProvidedService_POST_DTO providedServiceDTO)
        {
            if (_context.ProvidedServices == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.ProvidedService'  is null.");
            }

            var providedService = _mapper.Map<ProvidedService>(providedServiceDTO);
            _context.ProvidedServices.Add(providedService);
            await _context.SaveChangesAsync();

            return Ok(providedService);
        }

        // DELETE: api/ProvidedServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvidedService(int id)
        {
            if (_context.ProvidedServices == null)
            {
                return NotFound();
            }
            var providedService = await _context.ProvidedServices.FindAsync(id);
            if (providedService == null)
            {
                return NotFound();
            }

            _context.ProvidedServices.Remove(providedService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvidedServiceExists(int id)
        {
            return (_context.ProvidedServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
