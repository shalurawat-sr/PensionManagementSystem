using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PensionManagementSystem.Data;
using PensionManagementSystem.DTOs;
using PensionManagementSystem.Models;

namespace PensionManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowCrossSite]

    public class LoginsController : ControllerBase
    {
        private readonly PensionManagementSystemContext _context;

        public LoginsController(PensionManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/Logins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogin()
        {
          if (_context.Login == null)
          {
              return NotFound();
          }
            return await _context.Login.ToListAsync();
        }

        // GET: api/Logins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetLoginById(int id)
        {
          if (_context.Login == null)
          {
              return NotFound();
          }
            var login = await _context.Login.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return login;
        }

        // PUT: api/Logins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin(int id, Login login)
        {
            if (id != login.Id)
            {
                return BadRequest();
            }

            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
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

        // POST: api/Logins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login login)
        {
          if (_context.Login == null)
          {
              return Problem("Entity set 'PensionManagementSystemContext.Login'  is null.");
          }
            _context.Login.Add(login);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogin", new { id = login.Id }, login);
        }


        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<LoginValidate>> Login(LoginValidate login)
        {
            User user = new User();
            if (login.Email == "" || login.Password == "")
            {
                return BadRequest(new { Status = "fail", Message = "Email and Password cannot be empty" });
            }
            var users = _context.Login.Where(d => d.Email == login.Email).FirstOrDefault();

            if (users == null)
            {
                return BadRequest(new { Status = "fail", Message = "Email not found" });
            }
            if (login.Password == login.Password)
            {
                return Ok(new { Status = "success", Message = "Login Successful", User = users });
            }
            else
            {
                return BadRequest(new { Status = "fail", Message = "Password doesn't match" });
            }

        }





        // DELETE: api/Logins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin(int id)
        {
            if (_context.Login == null)
            {
                return NotFound();
            }
            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginExists(int id)
        {
            return (_context.Login?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
