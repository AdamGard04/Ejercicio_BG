using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP_Adam_Garcia_2024_09_10.Models;

namespace WebAPP_Adam_Garcia_2024_09_10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaContablesController : ControllerBase
    {
        private readonly ContabilidadContext _context;

        public CuentaContablesController(ContabilidadContext context)
        {
            _context = context;
        }

        // GET: api/CuentaContables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaContable>>> GetCuentaContables()
        {
          if (_context.CuentaContables == null)
          {
              return NotFound();
          }
            return await _context.CuentaContables.ToListAsync();
        }

        // GET: api/CuentaContables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaContable>> GetCuentaContable(int id)
        {
          if (_context.CuentaContables == null)
          {
              return NotFound();
          }
            var cuentaContable = await _context.CuentaContables.FindAsync(id);

            if (cuentaContable == null)
            {
                return NotFound();
            }

            return cuentaContable;
        }

        // PUT: api/CuentaContables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentaContable(int id, CuentaContable cuentaContable)
        {
            if (id != cuentaContable.CuentaId)
            {
                return BadRequest();
            }

            _context.Entry(cuentaContable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaContableExists(id))
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

        // POST: api/CuentaContables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CuentaContable>> PostCuentaContable(CuentaContable cuentaContable)
        {
          if (_context.CuentaContables == null)
          {
              return Problem("Entity set 'ContabilidadContext.CuentaContables'  is null.");
          }
            _context.CuentaContables.Add(cuentaContable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuentaContable", new { id = cuentaContable.CuentaId }, cuentaContable);
        }

        // DELETE: api/CuentaContables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuentaContable(int id)
        {
            if (_context.CuentaContables == null)
            {
                return NotFound();
            }
            var cuentaContable = await _context.CuentaContables.FindAsync(id);
            if (cuentaContable == null)
            {
                return NotFound();
            }

            _context.CuentaContables.Remove(cuentaContable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuentaContableExists(int id)
        {
            return (_context.CuentaContables?.Any(e => e.CuentaId == id)).GetValueOrDefault();
        }
    }
}
