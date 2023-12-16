using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BizzuiApi.Data;
using BizzuiApi.Models;

namespace Bizzui.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            var invoices = await _context.Invoices.ToListAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(long id)
        {
            if (id == 0) 
            {
                return BadRequest();
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Invoice>> CreateInvoice([FromBody]CreateInvoiceRqstMdl rqst)
        {   
            if (rqst == null) {
                return BadRequest();
            }
            var catalog = await _context.Catalogs.FindAsync(rqst.CatalogId);
            if (catalog == null)
            {
                return BadRequest("Invalid Catalog");
            }
            var customer = await _context.Customers.FindAsync(rqst.CustomerId);
            if (customer == null)
            {
                return BadRequest("Invalid Customer");
            }
            var StartDate = DateTime.Now.Date;
            var EndDate = StartDate.AddDays(catalog.ValidityDays);
            var DueDate = EndDate.AddDays(catalog.DueDays);
            var invoice = new Invoice(){
                CustomerId = rqst.CustomerId,
                CatalogId = rqst.CatalogId,
                Domain = rqst.Domain,
                StartDate = DateOnly.FromDateTime(StartDate),
                EndDate = DateOnly.FromDateTime(EndDate),
                DueDate = DateOnly.FromDateTime(DueDate),
                Status = "DRAFT",
            };
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            
            return Ok(invoice);
        }

        [HttpPost("{id}/update-domain")]
        public async Task<ActionResult<Invoice>> UpdateDomain(long id, string domain)
        {
            if (domain == null)
            {
                return BadRequest();
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) {
                return BadRequest();
            }
            
            invoice.Domain = domain;
            invoice.UpdatedAt = DateTime.Now;
            
            try
            {
                await _context.SaveChangesAsync();
                return Ok(invoice);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}",e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}/update-invoice-payment")]
        public async Task<ActionResult<Invoice>> UpdateInvoicePayment(long id, UpdateInvoicePaymentRqstMdl rqst)
        {
            if (rqst == null)
            {
                return BadRequest();
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) {
                return BadRequest();
            }
            
            invoice.Status = rqst.Status;
            invoice.PaymentType = rqst.PaymentType;
            invoice.UpdatedAt = DateTime.Now;
            
            try
            {
                await _context.SaveChangesAsync();
                return Ok(invoice);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}",e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteInvoice(long id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
