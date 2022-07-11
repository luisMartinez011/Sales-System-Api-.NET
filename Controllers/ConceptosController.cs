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
    public class ConceptosController : ControllerBase
    {
        private readonly dotnetventasContext _context;

        public ConceptosController(dotnetventasContext context)
        {
            _context = context;
        }

        // GET: api/Conceptos
        [HttpGet]
        public async Task<ActionResult<object>> GetConceptos()
        {
          if (_context.Conceptos == null)
          {
              return NotFound();
          }
            var salesdata = await _context.Conceptos
                //.Include(s => s.IdProductoNavigation)
                //.Select(s => new
                //{
                //    Id = s.Id,
                //    Cantidad = s.Cantidad,
                //    PrecioUnitario = s.IdProductoNavigation.Precio,
                //    Importe = s.Cantidad * s.IdProductoNavigation.Precio,
                //    Ventas = s.IdVentasNavigation
                //})
                .ToListAsync();
            return salesdata;
        }

        // GET: api/Conceptos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Concepto>> GetConcepto(long id)
        {
          if (_context.Conceptos == null)
          {
              return NotFound();
          }
            var concepto = await _context.Conceptos.FindAsync(id);

            if (concepto == null)
            {
                return NotFound();
            }

            return concepto;
        }

        // PUT: api/Conceptos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcepto(long id, Concepto concepto)
        {
            if (id != concepto.Id)
            {
                return BadRequest();
            }

            _context.Entry(concepto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConceptoExists(id))
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

        // POST: api/Conceptos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Concepto>> PostConcepto(Concepto concepto)
        {
          if (_context.Conceptos == null)
          {
              return Problem("Entity set 'dotnetventasContext.Conceptos'  is null.");
          }
            _context.Conceptos.Add(concepto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConcepto", new { id = concepto.Id }, concepto);
        }

        // DELETE: api/Conceptos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConcepto(long id)
        {
            if (_context.Conceptos == null)
            {
                return NotFound();
            }
            var concepto = await _context.Conceptos.FindAsync(id);
            if (concepto == null)
            {
                return NotFound();
            }

            _context.Conceptos.Remove(concepto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConceptoExists(long id)
        {
            return (_context.Conceptos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
