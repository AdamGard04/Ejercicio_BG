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
    public class MovimientoesController : ControllerBase
    {
        private readonly ContabilidadContext _context;

        public MovimientoesController(ContabilidadContext context)
        {
            _context = context;
        }

        // GET: api/Movimientoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientos()
        {
          if (_context.Movimientos == null)
          {
              return NotFound();
          }
            return await _context.Movimientos.ToListAsync();
        }

        // GET: api/Movimientoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
        {
          if (_context.Movimientos == null)
          {
              return NotFound();
          }
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return movimiento;
        }

        // PUT: api/Movimientoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, MovimientoDTO movimiento)
        {
            if (id != movimiento.AsientoId)
            {
                return BadRequest();
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoExists(id))
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

        // POST: api/Movimientoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovimientoDTO>> PostMovimiento(MovimientoDTO movimientoDTO)
        {
          if (_context.Movimientos == null)
          {
              return Problem("Entity set 'ContabilidadContext.Movimientos'  is null.");
          }
            
            var movimiento = new Movimiento
            {
                AsientoId = movimientoDTO.AsientoId,
                CuentaId = movimientoDTO.CuentaId,
                Valor = movimientoDTO.Valor,
                Descripcion = string.IsNullOrEmpty(movimientoDTO.Descripcion) ? null : movimientoDTO.Descripcion,
                TipoMovimiento = movimientoDTO.TipoMovimiento,
                // Agrega más campos si es necesario
            };
            _context.Movimientos.Add(movimiento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MovimientoExists(movimientoDTO.AsientoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovimiento", new { id = movimiento.AsientoId }, movimiento);
        }

        // DELETE: api/Movimientoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            if (_context.Movimientos == null)
            {
                return NotFound();
            }
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimientoExists(int id)
        {
            return (_context.Movimientos?.Any(e => e.AsientoId == id)).GetValueOrDefault();
        }
    }
}
