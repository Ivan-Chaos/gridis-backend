using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.MeterModel;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterModelsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public MeterModelsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/MeterModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeterModel_GET_DTO>>> GetMeterModels()
        {
            if (_context.Manufacturers == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<MeterModel_GET_DTO>>(_context.MeterModels.Include(mm => mm.Manufacturer).ToList());

            return Ok(data);
        }

        // GET: api/MeterModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeterModel_GET_DTO>> GetMeterModel(int id)
        {
            if (_context.MeterModels == null)
            {
                return NotFound();
            }
            var meterModel = await _context.MeterModels.Include(mm => mm.Manufacturer).FirstOrDefaultAsync(i => i.Id == id);

            if (meterModel == null)
            {
                return NotFound();
            }

            return _mapper.Map<MeterModel, MeterModel_GET_DTO>(meterModel);
        }

        // PUT: api/MeterModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeterModel(int id, MeterModel_POST_DTO meterModelDTO)
        {
            var meterModel = await _context.MeterModels.SingleOrDefaultAsync(t => t.Id == id);

            if (meterModel == null)
            {
                return NotFound();
            }

            _mapper.Map<MeterModel_POST_DTO, MeterModel>(meterModelDTO, meterModel);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeterModelExists(id))
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

        // POST: api/MeterModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MeterModel>> PostMeterModel(MeterModel_POST_DTO meterModelDTO)
        {
            if (_context.MeterModels == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.MeterModels'  is null.");
            }

            var meterModel = _mapper.Map<MeterModel>(meterModelDTO);
            _context.MeterModels.Add(meterModel);
            await _context.SaveChangesAsync();

            return Ok(meterModel);
        }

        // DELETE: api/MeterModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeterModel(int id)
        {
            if (_context.MeterModels == null)
            {
                return NotFound();
            }
            var meterModel = await _context.MeterModels.FindAsync(id);
            if (meterModel == null)
            {
                return NotFound();
            }

            _context.MeterModels.Remove(meterModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeterModelExists(int id)
        {
            return (_context.MeterModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
