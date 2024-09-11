using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPP_Adam_Garcia_2024_09_10.DTOs;
using WebAPP_Adam_Garcia_2024_09_10.Models;

namespace WebAPP_Adam_Garcia_2024_09_10.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AsientoContablesController : ControllerBase
    {
        private readonly ContabilidadContext _context;

        public AsientoContablesController(ContabilidadContext context)
        {
            _context = context;
        }

        // GET: api/AsientoContables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsientoContable>>> GetAsientoContables()
        {
          if (_context.AsientoContables == null)
          {
              return NotFound();
          }
            return await _context.AsientoContables.ToListAsync();
        }

        // GET: api/AsientoContables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AsientoContable>> GetAsientoContable(int id)
        {
          if (_context.AsientoContables == null)
          {
              return NotFound();
          }
            var asientoContable = await _context.AsientoContables.FindAsync(id);

            if (asientoContable == null)
            {
                return NotFound();
            }

            return asientoContable;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AsientoContable>>> GetAsientoContableFiltro(
    int? id, string? descripcion, DateTime? fecha, int? departamentoId)
        {
            if (_context.AsientoContables == null)
            {
                return NotFound();
            }

            var query = _context.AsientoContables.AsQueryable();

            // Filtros opcionales
            if (id.HasValue)
            {
                query = query.Where(a => a.Id == id.Value);
            }

            if (!string.IsNullOrEmpty(descripcion))
            {
                query = query.Where(a => a.Descripcion.Contains(descripcion));
            }

            if (fecha.HasValue)
            {
                query = query.Where(a => a.Fecha == fecha.Value);
            }

            if (departamentoId.HasValue)
            {
                query = query.Where(a => a.DepartamentoId == departamentoId.Value);
            }

            var resultados = await query.ToListAsync();

            if (resultados.Count == 0)
            {
                return NotFound();
            }

            return Ok(resultados);
        }

        // PUT: api/AsientoContables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsientoContable(int id, AsientoContableDTO asientoContable)
        {
            if (id != asientoContable.Id)
            {
                return BadRequest();
            }

            _context.Entry(asientoContable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsientoContableExists(id))
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

        // POST: api/AsientoContables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AsientoContableDTO>> PostAsientoContable(AsientoContableDTO asientoContable)
        {
          if (_context.AsientoContables == null)
          {
              return Problem("Entity set 'ContabilidadContext.AsientoContables'  is null.");
          }
            var asientoContables = new AsientoContable
            {
                Id = asientoContable.Id,
                Fecha = asientoContable.Fecha,
                DepartamentoId = asientoContable.DepartamentoId,
                Descripcion = asientoContable.Descripcion,
                Estado = asientoContable.Estado,
            };
            _context.AsientoContables.Add(asientoContables);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsientoContable", new { id = asientoContable.Id }, asientoContable);
        }

        // DELETE: api/AsientoContables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsientoContable(int id)
        {
            if (_context.AsientoContables == null)
            {
                return NotFound();
            }
            var asientoContable = await _context.AsientoContables.FindAsync(id);
            if (asientoContable == null)
            {
                return NotFound();
            }

            _context.AsientoContables.Remove(asientoContable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsientoContableExists(int id)
        {
            return (_context.AsientoContables?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
