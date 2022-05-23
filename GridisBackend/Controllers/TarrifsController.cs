using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Tarrif;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarrifsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public TarrifsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tarrifs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarrif_GET_POST_DTO>>> GetTarrifs()
        {
            if (_context.Tarrifs == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Tarrif_GET_POST_DTO>>(_context.Tarrifs.ToList());

            return Ok(data);
        }

        // GET: api/Tarrifs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarrif_GET_POST_DTO>> GetTarrif(int id)
        {
            if (_context.Tarrifs == null)
            {
                return NotFound();
            }
            var tarrif = await _context.Tarrifs.FirstOrDefaultAsync(i => i.Id == id);

            if (tarrif == null)
            {
                return NotFound();
            }

            return _mapper.Map<Tarrif, Tarrif_GET_POST_DTO>(tarrif);
        }

        // PUT: api/Tarrifs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarrif(int id, Tarrif_GET_POST_DTO tarrifDTO)
        {
            var tarrif = await _context.Tarrifs.SingleOrDefaultAsync(t => t.Id == id);

            if (tarrif == null)
            {
                return NotFound();
            }

            _mapper.Map<Tarrif_GET_POST_DTO, Tarrif>(tarrifDTO, tarrif);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarrifExists(id))
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

        // POST: api/Tarrifs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tarrif>> PostTarrif(Tarrif_GET_POST_DTO tarrifDTO)
        {
            if (_context.Tarrifs == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Tarrifs'  is null.");
            }

            var tarrif = _mapper.Map<Tarrif>(tarrifDTO);
            _context.Tarrifs.Add(tarrif);
            await _context.SaveChangesAsync();

            return Ok(tarrif);
        }

        // DELETE: api/Tarrifs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarrif(int id)
        {
            if (_context.Tarrifs == null)
            {
                return NotFound();
            }
            var tarrif = await _context.Tarrifs.FindAsync(id);
            if (tarrif == null)
            {
                return NotFound();
            }

            _context.Tarrifs.Remove(tarrif);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarrifExists(int id)
        {
            return (_context.Tarrifs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
