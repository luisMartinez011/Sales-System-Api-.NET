using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventas.Models;

namespace Ventas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly dotnetventasContext _context;

        public VentasController(dotnetventasContext context)
        {
            _context = context;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<object>> GetVentas()
        {
          if (_context.Ventas == null)
          {
              return NotFound();
          }
            var salesdata = await _context.Ventas
                  .Include(s => s.Conceptos)
                  .Select(s => new
                  {
                      Id = s.Id,
                      fecha = s.Fecha,
                      total = s.Total,
                      idCliente = s.IdCliente,
                      Cliente = s.IdClienteNavigation.Name,
                      Conceptos = s.Conceptos
                  })
                  .ToListAsync();
            return salesdata;
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(long id)
        {
          if (_context.Ventas == null)
          {
              return NotFound();
          }
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // PUT: api/Ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(long id, Venta venta)
        {
            if (id != venta.Id)
            {
                return BadRequest();
            }

            _context.Entry(venta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(id))
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

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Venta>> PostVenta(Venta venta)
        {
          if (_context.Ventas == null)
          {
              return Problem("Entity set 'dotnetventasContext.Ventas'  is null.");
          }
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVenta", new { id = venta.Id }, venta);
        }

        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(long id)
        {
            if (_context.Ventas == null)
            {
                return NotFound();
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentaExists(long id)
        {
            return (_context.Ventas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
