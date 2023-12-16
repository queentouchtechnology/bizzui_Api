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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Catalog>>> GetCatalogs()
        {
            var catalogs = await _context.Catalogs.ToListAsync();
            return Ok(catalogs);
        }

        // GET: api/Catalog/sql
        [HttpGet("sql")]
        public async Task<ActionResult<IEnumerable<Catalog>>> ExecuteSqlQuery()
        {
            var sql = "SELECT * FROM Catalogs";
            var catalogs = await _context.Catalogs.FromSqlRaw(sql).ToListAsync();
            return Ok(catalogs);
        }

        // GET: api/Catalog/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // [ProducesResponseType(200, Type = typeof(Catalog))]
        public async Task<ActionResult<Catalog>> GetCatalog(long id)
        {
            if (id == 0) 
            {
                return BadRequest();
            }
            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }

            return Ok(catalog);
        }

        // POST: api/Catalog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<ActionResult<Catalog>> CreateCatalog([FromBody]CatalogDto rqst)
        {   
            if (rqst == null) {
                return BadRequest();
            }
            if (!(rqst.Type == "Service" || rqst.Type == "Product")) {
                return BadRequest("Invalid Type");
            }
            var catalog = new Catalog(){
                Id = 0,
                Name = rqst.Name,
                Description = rqst.Description,
                Platform = rqst.Platform,
                Type = rqst.Type,
                Price = rqst.Price, 
                ValidityDays = rqst.ValidityDays,
                DueDays = rqst.DueDays,
                Active = true,
            };
            _context.Catalogs.Add(catalog);
            await _context.SaveChangesAsync();
            return Ok(catalog);
        }

        // PUT: api/Catalog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}/update")]
        public async Task<ActionResult<Catalog>> UpdateCatalog(long id, [FromBody]CatalogDto rqst)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog == null) {
                return BadRequest();
            }
            
            catalog.Name = rqst.Name;
            catalog.Description = rqst.Description;
            catalog.Platform = rqst.Platform;
            catalog.Type = rqst.Type;
            catalog.Price = rqst.Price;
            catalog.ValidityDays = rqst.ValidityDays;
            catalog.DueDays = rqst.DueDays;
            catalog.UpdatedAt = DateTime.Now;
            
            try
            {
                await _context.SaveChangesAsync();
                return Ok(catalog);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}",e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Catalog/5
        [HttpPost("{id}/delete")]
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
    }
}
