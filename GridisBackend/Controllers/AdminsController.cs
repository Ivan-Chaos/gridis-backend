using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GridisBackend.Models;
using AutoMapper;
using GridisBackend.DTOs.Admin;

namespace GridisBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly PowerManagementOLTPContext _context;
        private IMapper _mapper;

        public AdminsController(PowerManagementOLTPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin_GET_DTO>>> GetAdmins()
        {
            if (_context.Admins == null)
            {
                return NotFound();
            }
            var data = _mapper.Map<List<Admin_GET_DTO>>(_context.Admins.Include(a => a.Person).ToList());

            return Ok(data);
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin_GET_DTO>> GetAdmin(int id)
        {
            if (_context.Admins == null)
            {
                return NotFound();
            }
            var admin = await _context.Admins.Include(a => a.Person).FirstOrDefaultAsync(i => i.Id == id);

            if (admin == null)
            {
                return NotFound();
            }

            return _mapper.Map<Admin, Admin_GET_DTO>(admin);
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin_POST_DTO adminDTO)
        {
            var admin = await _context.Admins.SingleOrDefaultAsync(t => t.Id == id);

            if (admin == null)
            {
                return NotFound();
            }

            _mapper.Map<Admin_POST_DTO, Admin>(adminDTO, admin);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
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

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin_POST_DTO adminDTO)
        {
            if (_context.Admins == null)
            {
                return Problem("Entity set 'PowerManagementOLTPContext.Admin'  is null.");
            }

            var admin = _mapper.Map<Admin>(adminDTO);
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return Ok(admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if (_context.Admins == null)
            {
                return NotFound();
            }
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return (_context.Admins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
