#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using WebApp.Data;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashesControllerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CashesControllerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CashesController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cash>>> GetCashes()
        {
            return await _context.Cashes.ToListAsync();
        }

        // GET: api/CashesController/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cash>> GetCash(Guid id)
        {
            var cash = await _context.Cashes.FindAsync(id);

            if (cash == null)
            {
                return NotFound();
            }

            return cash;
        }

        // PUT: api/CashesController/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCash(Guid id, Cash cash)
        {
            if (id != cash.Id)
            {
                return BadRequest();
            }

            _context.Entry(cash).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CashExists(id))
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

        // POST: api/CashesController
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cash>> PostCash(Cash cash)
        {
            _context.Cashes.Add(cash);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCash", new { id = cash.Id }, cash);
        }

        // DELETE: api/CashesController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCash(Guid id)
        {
            var cash = await _context.Cashes.FindAsync(id);
            if (cash == null)
            {
                return NotFound();
            }

            _context.Cashes.Remove(cash);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CashExists(Guid id)
        {
            return _context.Cashes.Any(e => e.Id == id);
        }
    }
}
