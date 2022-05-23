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
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Stock>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Stock>> GetStocks()
        {
            return await _bll.Stocks.PublicGetAllAsync(User.GetUserId());
        }

        // GET: api/Stocks/5
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
