using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.ServiceRequest;
using GridisBackend.DTOs.ProvidedService;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public ServiceRequestsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ServiceRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequest_POST_DTO>>> GetServiceRequests()
        {
            if (_context.ServiceRequests == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<ServiceRequest_POST_DTO>>(_context.ServiceRequests.ToList());

            return Ok(data);
        }

        // GET: api/ServiceRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequest_GET_DTO>> GetServiceRequest(int id)
        {
            if (_context.ServiceRequests == null)
            {
                return NotFound();
            }
            var serviceRequest = await _context.ServiceRequests.Include(sr=>sr.Service).Include(sr=>sr.CallOperator).Include(sr=>sr.Residence).Include(sr=>sr.MeterModel).FirstOrDefaultAsync(i => i.Id == id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return _mapper.Map<ServiceRequest, ServiceRequest_GET_DTO>(serviceRequest);
        }

        // PUT: api/ServiceRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRequest(int id, ServiceRequest_POST_DTO serviceRequestDTO)
        {
            var serviceRequest = await _context.ServiceRequests.SingleOrDefaultAsync(t => t.Id == id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            _mapper.Map<ServiceRequest_POST_DTO, ServiceRequest>(serviceRequestDTO, serviceRequest);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestExists(id))
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

        // POST: api/ServiceRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServiceRequest>> PostServiceRequest(ServiceRequest_POST_DTO serviceRequestDTO)
        {
            if (_context.ServiceRequests == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.ServiceRequests'  is null.");
            }

            var serviceRequest = _mapper.Map<ServiceRequest>(serviceRequestDTO);
            var insertedServiceRequest = _context.ServiceRequests.Add(serviceRequest);

            var residence = _mapper.Map<Residence>(await _context.Residences.Include(r => r.Address).ThenInclude(a => a.Street).ThenInclude(s => s.District).ThenInclude(d => d.City).FirstOrDefaultAsync(i => i.Id == serviceRequest.ResidenceId));
            var engineer = await _context.Engineers.OrderByDescending(e => e.ProvidedServices.ToList().Count)
                .SingleOrDefaultAsync(e => e.DistrictId == residence.Address.Street.District.Id);
            
            if (engineer == null)
            {
                engineer = await _context.Engineers.Include(e=>e.District).ThenInclude(d=>d.City).OrderByDescending(e=>e.ProvidedServices.ToList().Count)
                    .SingleOrDefaultAsync(e => e.District.City.Id == residence.Address.Street.District.City.Id);
            
                if(engineer== null)
                {
                    return Problem("NO ENGINEER");
                }
            }

            await _context.SaveChangesAsync();

            var providedServiceDTO = new ProvidedService_POST_DTO
            {
                CompletedTime = null,
                ServiceRequestId = serviceRequest.Id,
                EngineerId = engineer.Id,
                IsCompleted = false
            };

            _context.ProvidedServices.Add(_mapper.Map<ProvidedService>(providedServiceDTO));

            await _context.SaveChangesAsync();


            return Ok();

        }

        // DELETE: api/ServiceRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
            if (_context.ServiceRequests == null)
            {
                return NotFound();
            }
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            _context.ServiceRequests.Remove(serviceRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceRequestExists(int id)
        {
            return (_context.ServiceRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
