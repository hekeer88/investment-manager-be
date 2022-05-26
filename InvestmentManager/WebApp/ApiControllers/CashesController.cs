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
        /// <summary>
        /// Get all Cash entities related with user
        /// </summary>
        /// <returns>List of cash entities</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Cash>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Cash>> GetCashes()
        {
            return await _bll.Cashes.PublicGetAllAsync(User.GetUserId());
        }

        // GET: api/Cashes/5
        /// <summary>
        /// Get Cash entity by cash ID
        /// </summary>
        /// <param name="id">Cash id</param>
        /// <returns>Cash entity by id</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Cash), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.Public.DTO.v1.Cash>> GetCash(Guid id)
        {
            var cash = await _bll.Cashes.PublicFirstOrDefaultAsync(id);
            
            if (cash == null)
            {
                return NotFound();
            }

            return cash;
        }

        // PUT: api/Cashes/5
        /// <summary>
        /// Change cash entity
        /// </summary>
        /// <param name="id">Cash id</param>
        /// <param name="cash">Cash entity that need to be changed</param>
        /// <returns>Result</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCash(Guid id,  App.Public.DTO.v1.Cash cash)
        {
            if (id != cash.Id)
            {
                return BadRequest();
            }

            _bll.Cashes.Update(cash);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CashExists(id))
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
        /// <summary>
        /// Post new Cash entity
        /// </summary>
        /// <param name="cash">Supply - currency, portfolioId </param>
        /// <returns>New Cash Entity</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Cash), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cash>> PostCash([FromBody] App.Public.DTO.v1.Cash cash)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            
            _bll.Cashes.Add(cash);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCash",
                new
                {
                    id = cash.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }, cash);
        }

        // DELETE: api/Cashes/5
        /// <summary>
        /// Deletes Cash entity by id
        /// </summary>
        /// <param name="id">Cash id</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCash(Guid id)
        {
            await _bll.Cashes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> CashExists(Guid id)
        {
            return await _bll.Cashes.ExistsAsync(id);
        }
    }
}
