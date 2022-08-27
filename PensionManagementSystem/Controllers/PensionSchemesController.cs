using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PensionManagementSystem.Data;
using PensionManagementSystem.Models;

namespace PensionManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowCrossSite]

    public class PensionSchemesController : ControllerBase
    {
        private readonly PensionManagementSystemContext _context;

        public PensionSchemesController(PensionManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/PensionSchemes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PensionScheme>>> GetPensionScheme()
        {
          if (_context.PensionScheme == null)
          {
              return NotFound();
          }
            return await _context.PensionScheme.ToListAsync();
        }

        // GET: api/PensionSchemes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PensionScheme>> GetPensionScheme(int id)
        {
          if (_context.PensionScheme == null)
          {
              return NotFound();
          }
            var pensionScheme = await _context.PensionScheme.FindAsync(id);

            if (pensionScheme == null)
            {
                return NotFound();
            }

            return pensionScheme;
        }

        // PUT: api/PensionSchemes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPensionScheme(int id, PensionScheme pensionScheme)
        {
            if (id != pensionScheme.Id)
            {
                return BadRequest();
            }

            _context.Entry(pensionScheme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PensionSchemeExists(id))
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

        // POST: api/PensionSchemes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PensionScheme>> PostPensionScheme(PensionScheme pensionScheme)
        {
          if (_context.PensionScheme == null)
          {
              return Problem("Entity set 'PensionManagementSystemContext.PensionScheme'  is null.");
          }
            _context.PensionScheme.Add(pensionScheme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPensionScheme", new { id = pensionScheme.Id }, pensionScheme);
        }

        // DELETE: api/PensionSchemes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePensionScheme(int id)
        {
            if (_context.PensionScheme == null)
            {
                return NotFound();
            }
            var pensionScheme = await _context.PensionScheme.FindAsync(id);
            if (pensionScheme == null)
            {
                return NotFound();
            }

            _context.PensionScheme.Remove(pensionScheme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PensionSchemeExists(int id)
        {
            return (_context.PensionScheme?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
