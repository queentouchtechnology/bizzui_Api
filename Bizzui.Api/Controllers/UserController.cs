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
    public class UserController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRspnMdl>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            List<UserRspnMdl> rspn = [];
            for (int i = 0; i < users.Count; i++) {
                rspn.Add(new UserRspnMdl() {
                    Id = users[i].Id,
                    Email = users[i].Email,
                    Mobile = users[i].Mobile,
                    Role = users[i].Role,
                    CreatedAt = users[i].CreatedAt,
                    UpdatedAt = users[i].UpdatedAt
                });
            }
            return Ok(rspn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRspnMdl>> GetUser(long id)
        {
            if (id == 0) 
            {
                return BadRequest();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var rspn = new UserRspnMdl(){
                Id = user.Id,
                Email = user.Email,
                Mobile = user.Mobile,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
            return Ok(rspn);
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserRspnMdl>> CreateCatalog([FromBody]UserRqstMdl rqst)
        {   
            if (rqst == null) {
                return BadRequest();
            }
            var user = new User(){
                Email = rqst.Email,
                Password = rqst.Password,
                Mobile = rqst.Mobile,
                Role = rqst.Role
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var rspn = new UserRspnMdl(){
                Id = user.Id,
                Email = user.Email,
                Mobile = user.Mobile,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
            return Ok(rspn);
        }

        [HttpPost("{id}/update")]
        public async Task<ActionResult<UserRspnMdl>> UpdateUser(long id, [FromBody]UserRqstMdl rqst)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                return BadRequest();
            }
            
            user.Email = rqst.Email;
            user.Mobile = rqst.Mobile;
            user.Role = rqst.Role;
            user.UpdatedAt = DateTime.Now;
            
            try
            {
                await _context.SaveChangesAsync();
                var rspn = new UserRspnMdl(){
                    Id = user.Id,
                    Email = user.Email,
                    Mobile = user.Mobile,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };
                return Ok(rspn);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}",e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
