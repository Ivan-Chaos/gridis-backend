using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Payment;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public PaymentsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment_GET_POST_DTO>>> GetPayments()
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Payment_GET_POST_DTO>>(_context.Payments.ToList());

            return Ok(data);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment_GET_POST_DTO>> GetPayment(int id)
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }
            var payment = await _context.Payments.FirstOrDefaultAsync(i => i.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            return _mapper.Map<Payment, Payment_GET_POST_DTO>(payment);
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment_GET_POST_DTO paymentDTO)
        {
            var payment = await _context.Payments.SingleOrDefaultAsync(t => t.Id == id);

            if (payment == null)
            {
                return NotFound();
            }

            _mapper.Map<Payment_GET_POST_DTO, Payment>(paymentDTO, payment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment_GET_POST_DTO paymentDTO)
        {
            if (_context.Payments == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Payment'  is null.");
            }

            var payment = _mapper.Map<Payment>(paymentDTO);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return (_context.Payments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
