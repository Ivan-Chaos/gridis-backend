using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.CallOperator;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallOperatorsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public CallOperatorsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CallOperators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CallOperator_GET_DTO>>> GetCallOperators()
        {
            if (_context.CallOperators == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<CallOperator_GET_DTO>>(_context.CallOperators.Include(co => co.Person).ToList());

            return Ok(data);
        }

        // GET: api/CallOperators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CallOperator_GET_DTO>> GetCallOperator(int id)
        {
            if (_context.CallOperators == null)
            {
                return NotFound();
            }
            var callOperator = await _context.CallOperators.Include(co => co.Person).FirstOrDefaultAsync(i => i.Id == id);

            if (callOperator == null)
            {
                return NotFound();
            }

            return _mapper.Map<CallOperator, CallOperator_GET_DTO>(callOperator);
        }

        // PUT: api/CallOperators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCallOperator(int id, CallOperator_POST_DTO callOperatorDTO)
        {
            var callOperator = await _context.CallOperators.SingleOrDefaultAsync(t => t.Id == id);

            if (callOperator == null)
            {
                return NotFound();
            }

            _mapper.Map<CallOperator_POST_DTO, CallOperator>(callOperatorDTO, callOperator);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallOperatorExists(id))
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

        // POST: api/CallOperators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CallOperator>> PostCallOperator(CallOperator_POST_DTO callOperatorDTO)
        {
            if (_context.CallOperators == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.CallOperators'  is null.");
            }

            var callOperator = _mapper.Map<CallOperator>(callOperatorDTO);
            _context.CallOperators.Add(callOperator);
            await _context.SaveChangesAsync();

            return Ok(callOperator);
        }

        // DELETE: api/CallOperators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCallOperator(int id)
        {
            if (_context.CallOperators == null)
            {
                return NotFound();
            }
            var callOperator = await _context.CallOperators.FindAsync(id);
            if (callOperator == null)
            {
                return NotFound();
            }

            _context.CallOperators.Remove(callOperator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CallOperatorExists(int id)
        {
            return (_context.CallOperators?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
