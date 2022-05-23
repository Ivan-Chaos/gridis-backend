using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Bill;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public BillsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill_GET_DTO>>> GetBills()
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Bill_GET_DTO>>(_context.Bills.Include(b => b.Tarrif).Include(b=>b.Readings).ThenInclude(r=>r.InstalledMeter).ThenInclude(im=>im.Model).ThenInclude(mm=>mm.Manufacturer).ToList());

            return Ok(data);
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bill_GET_DTO>> GetBill(int id)
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }
            var bill = await _context.Bills.Include(b => b.Tarrif).Include(b => b.Readings).ThenInclude(r => r.InstalledMeter).ThenInclude(im => im.Model).ThenInclude(mm => mm.Manufacturer).FirstOrDefaultAsync(i => i.Id == id);

            if (bill == null)
            {
                return NotFound();
            }

            return _mapper.Map<Bill, Bill_GET_DTO>(bill);
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bill_POST_DTO billDTO)
        {
            var bill = await _context.Bills.SingleOrDefaultAsync(t => t.Id == id);

            if (bill == null)
            {
                return NotFound();
            }

            _mapper.Map<Bill_POST_DTO, Bill>(billDTO, bill);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(Bill_POST_DTO billDTO)
        {
            if (_context.Bills == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Bills'  is null.");
            }

            var bill = _mapper.Map<Bill>(billDTO);
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            return Ok(bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillExists(int id)
        {
            return (_context.Bills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
