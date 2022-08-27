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

    public class UsersController : ControllerBase
    {
        private readonly PensionManagementSystemContext _context;

        public UsersController(PensionManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            var user = await _context.User.FindAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{email}")]
        public async Task<IActionResult> PutUser(string email, RequestValidate requestValidate)
        {
            
            DateTime today = DateTime.Now; // 12/20/2015 11:48:09 AM  
            var pensionId = Guid.NewGuid().ToString("n").Substring(0, 8);

            PensionStatus pensionStatus = new PensionStatus();
            Request request = new Request();
           /* User user = new User();*/
            var user = await _context.User.FindAsync(email);


            if (user == null)
            {
                return BadRequest();
            }
            pensionStatus.PensionName = requestValidate.PensionName;
            pensionStatus.PensionId = pensionId;
            pensionStatus.Active = "Not Active";
            pensionStatus.Status = "Waitting";
            pensionStatus.Email = requestValidate.Email;

            request.Description = requestValidate.Description;
            request.PensionName = requestValidate.PensionName;
            request.PensionId = pensionId;
            request.RequestDate = today;
            user.BankName = requestValidate.BankName;
            user.BankType = requestValidate.BankType;
            user.AccountNumber = requestValidate.AccountNumber;
            user.Aadhar = requestValidate.Aadhar;
            user.PAN = requestValidate.PAN;

            _context.Request.Add(request);
            _context.PensionStatus.Add(pensionStatus);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(new { Status = "fail", Message = ex});
                if (!UserExists(email))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.User == null)
          {
              return Problem("Entity set 'PensionManagementSystemContext.User'  is null.");
          }
            _context.User.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Email }, user);
        }




        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<Register>> Register(Register register)
        {
            DateTime today = DateTime.Now; // 12/20/2015 11:48:09 AM  
            /*Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();
*/
            User users = new User();
            Login login = new Login();

            users.Email = register.Email;
            users.Age = register.Age;
            users.Address = register.Address;
            users.FullName = register.FullName;
            users.Gender = register.Gender;
            users.Mobile = register.Mobile;
            users.RegisterDate = today;
            login.Roles = "User";
            login.Password = register.Password;
            login.Email = register.Email;


            _context.User.Add(users);
            _context.Login.Add(login);

            await _context.SaveChangesAsync();

            return register;
        }



        [Route("showUser/{aadhar}")]
        [HttpGet]
        public async Task<ActionResult<User>> GetUserByAadhar(string aadhar)
        {
            var user = await _context.User.FindAsync(aadhar);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }





        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.User?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
