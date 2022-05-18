#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using App.DAL.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CashesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CashesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Cashes
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Cash>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Cash>> GetCashes()
        {
            return await _bll.Cashes.PublicGetAllAsync(User.GetUserId());
        }

        // GET: api/Cashes/5
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

        // PUT: api/Cashes/5
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

        // POST: api/Cashes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cash>> PostCash(Cash cash)
        {
            _context.Cashes.Add(cash);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCash", new { id = cash.Id }, cash);
        }

        // DELETE: api/Cashes/5
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
