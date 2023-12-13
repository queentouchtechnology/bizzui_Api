using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BizzuiApi.Data;
using BizzuiApi.Models;

namespace Bizzui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/Catalog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catalog>>> GetCatalogs()
        {
            return await _context.Catalogs.ToListAsync();
        }

        // GET: api/Catalog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Catalog>> GetCatalog(long id)
        {
            var catalog = await _context.Catalogs.FindAsync(id);

            if (catalog == null)
            {
                return NotFound();
            }

            return catalog;
        }

        // PUT: api/Catalog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalog(long id, Catalog catalog)
        {
            if (id != catalog.Id)
            {
                return BadRequest();
            }

            _context.Entry(catalog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogExists(id))
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

        // POST: api/Catalog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Catalog>> PostCatalog(Catalog catalog)
        {
            _context.Catalogs.Add(catalog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCatalog", new { id = catalog.Id }, catalog);
        }

        // DELETE: api/Catalog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalog(long id)
        {
            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }

            _context.Catalogs.Remove(catalog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogExists(long id)
        {
            return _context.Catalogs.Any(e => e.Id == id);
        }
    }
}
