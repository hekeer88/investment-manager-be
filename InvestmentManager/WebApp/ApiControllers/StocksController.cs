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
    public class StocksController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public StocksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Stocks
        /// <summary>
        /// Get all Stock entities related with user
        /// </summary>
        /// <returns>All Stock entities related with user</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Stock>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Stock>> GetStocks()
        {
            return await _bll.Stocks.PublicGetAllAsync(User.GetUserId());
        }

        // GET: api/Stocks/5
        /// <summary>
        /// Get Stock entity by it id   
        /// </summary>
        /// <param name="id">Stock entity id</param>
        /// <returns>Stock entity</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Stock), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.Public.DTO.v1.Stock>> GetStock(Guid id)
        {
            var stock = await _bll.Stocks.PublicFirstOrDefaultAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        // PUT: api/Stocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Change Stock entity by its id
        /// </summary>
        /// <param name="id">Stock entity id that need to be changed</param>
        /// <param name="stock">Stock entity that need to be changed</param>
        /// <returns>Result</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutStock(Guid id, App.Public.DTO.v1.Stock stock)
        {
            if (id != stock.Id)
            {
                return BadRequest();
            }

            _bll.Stocks.Update(stock);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StockExists(id))
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

        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post new Stock entity and save to the database
        /// </summary>
        /// <param name="stock">Provide company name, ticker, comment, regionId, portfolioId, industryId</param>
        /// <returns>Newly created Stock entity</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Stock), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<App.BLL.DTO.Stock>> PostStock(App.Public.DTO.v1.Stock stock)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            
            _bll.Stocks.Add(stock);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetStock", new
            {
                id = stock.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, stock);
        }

        // DELETE: api/Stocks/5
        /// <summary>
        /// Delete Stock entity by its id
        /// </summary>
        /// <param name="id">Stock entity id that needs to be deleted</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStock(Guid id)
        {
            await _bll.Stocks.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> StockExists(Guid id)
        {
            return await _bll.Stocks.ExistsAsync(id);
        }
    }
}
