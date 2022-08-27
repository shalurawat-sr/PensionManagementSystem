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

    public class PensionStatusController : ControllerBase
    {
        private readonly PensionManagementSystemContext _context;

        public PensionStatusController(PensionManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/PensionStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PensionStatus>>> GetPensionStatus()
        {
          if (_context.PensionStatus == null)
          {
              return NotFound();
          }
            return await _context.PensionStatus.ToListAsync();
        }


        [Route("ByOfficer")]
        [HttpGet]
        public async Task<Object> GetPensionStatusByOfficer()
        {
            var users = _context.PensionStatus
               .Join(
                   _context.Request,
                   status => status.PensionId,
                   request => request.PensionId,
                   (status, request) => new
                   {
                       PensionId = status.PensionId,
                       PensionName = status.PensionName,
                       Email = status.Email,
                       Status = status.Status,
                       Active = status.Active,
                       RequestDate = request.RequestDate,
                       RequestId = request.RequestId,
                       Description = request.Description,
                   }
               ).ToList();

            return users;
        }



        [Route("ShowStatus/{pensionId}")]
        [HttpPut]
        public async Task<IActionResult> UpdatePensionStatus(string pensionId)
        {
            var pensionStatus = await _context.PensionStatus.FindAsync(pensionId);

            if (pensionStatus == null)
            {
                return NotFound();
            }
                pensionStatus.Active = "Active";
                pensionStatus.PensionReleaseDate = DateTime.Now;
                _context.Entry(pensionStatus).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PensionStatusExists(pensionId))
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


        [Route("CancelPension/{pensionId}")]
        [HttpPut]
        public async Task<IActionResult> UpdatePensionCancellation(string pensionId)
        {
            var pensionStatus = await _context.PensionStatus.FindAsync(pensionId);

            if (pensionStatus == null)
            {
                return NotFound();
            }
            pensionStatus.Status = "Pension Request Failed ";
            _context.Entry(pensionStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PensionStatusExists(pensionId))
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








        [Route("UpdateStatus/{pensionId}")]
        [HttpPut]
        public async Task<IActionResult> PutPensionStatus(string pensionId)
        {
            var pensionStatus = await _context.PensionStatus.FindAsync(pensionId);

            if (pensionStatus == null)
            {
                return NotFound();
            }
            pensionStatus.Status = "Approved";
            pensionStatus.ApprovedBy = "Officer";


            _context.Entry(pensionStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PensionStatusExists(pensionId))
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










        // GET: api/PensionStatus/5
        [HttpGet("{email}")]
        public async Task<ActionResult<PensionStatus>> GetPensionStatus(string email)
        {
            var pensionStatus = await _context.PensionStatus.FindAsync(email);

            
            if (pensionStatus == null)
            {
                return NotFound();
            }

            return pensionStatus;
        }







        [Route("ByAdmin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PensionStatus>>> GetPensionStatusByAdmin()
        {
            if (_context.PensionStatus == null)
            {
                return NotFound();
            }
            return await _context.PensionStatus.ToListAsync();
        }






        // PUT: api/PensionStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPensionStatus(string id, PensionStatus pensionStatus)
        {
            if (id != pensionStatus.PensionId)
            {
                return BadRequest();
            }

            _context.Entry(pensionStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PensionStatusExists(id))
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








        // POST: api/PensionStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PensionStatus>> PostPensionStatus(PensionStatus pensionStatus)
        {
          if (_context.PensionStatus == null)
          {
              return Problem("Entity set 'PensionManagementSystemContext.PensionStatus'  is null.");
          }
            _context.PensionStatus.Add(pensionStatus);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PensionStatusExists(pensionStatus.PensionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPensionStatus", new { id = pensionStatus.PensionId }, pensionStatus);
        }

        // DELETE: api/PensionStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePensionStatus(string id)
        {
            if (_context.PensionStatus == null)
            {
                return NotFound();
            }
            var pensionStatus = await _context.PensionStatus.FindAsync(id);
            if (pensionStatus == null)
            {
                return NotFound();
            }

            _context.PensionStatus.Remove(pensionStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PensionStatusExists(string id)
        {
            return (_context.PensionStatus?.Any(e => e.PensionId == id)).GetValueOrDefault();
        }
    }
}
