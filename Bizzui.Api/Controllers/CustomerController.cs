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
    public class CustomerController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(long id)
        {
            if (id == 0) 
            {
                return BadRequest();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody]CustomerRqstMdl rqst)
        {   
            if (rqst == null) {
                return BadRequest();
            }
            var customer = new Customer(){
                Title = rqst.Title,
                FirstName = rqst.FirstName,
                LastName = rqst.LastName,
                Email = rqst.Email,
                Mobile = rqst.Mobile,
                CompanyName = rqst.CompanyName,
                Address1 = rqst.Address1,
                Address2 = rqst.Address2,
                City = rqst.City,
                State = rqst.State,
                Country = rqst.Country,
                Pincode = rqst.Pincode,
                AadharNo = rqst.AadharNo,
                PanNo = rqst.PanNo,
                GstNo = rqst.GstNo
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            
            return Ok(customer);
        }

        [HttpPost("{id}/update")]
        public async Task<ActionResult<Customer>> UpdateUser(long id, [FromBody]CustomerRqstMdl rqst)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) {
                return BadRequest();
            }
            
            customer.Title = rqst.Title;
            customer.FirstName = rqst.FirstName;
            customer.LastName = rqst.LastName;
            customer.Email = rqst.Email;
            customer.Mobile = rqst.Mobile;
            customer.CompanyName = rqst.CompanyName;
            customer.Address1 = rqst.Address1;
            customer.Address2 = rqst.Address2;
            customer.City = rqst.City;
            customer.State = rqst.State;
            customer.Country = rqst.Country;
            customer.Pincode = rqst.Pincode;
            customer.AadharNo = rqst.AadharNo;
            customer.PanNo = rqst.PanNo;
            customer.GstNo = rqst.GstNo;
            customer.UpdatedAt = DateTime.Now;
            
            try
            {
                await _context.SaveChangesAsync();
                return Ok(customer);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}",e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
