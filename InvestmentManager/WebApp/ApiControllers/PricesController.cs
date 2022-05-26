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
    public class PricesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Prices
        /// <summary>
        /// Get all Price entities related with user
        /// </summary>
        /// <returns>All Price entities related with user</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Price>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Price>> GetPrices()
        {
            return await _bll.Prices.PublicGetAllAsync(User.GetUserId());
        }

        // GET: api/Prices/5
        /// <summary>
        /// Get Price entity by id
        /// </summary>
        /// <param name="id">Price entity id</param>
        /// <returns>Price entity</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Price), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.Public.DTO.v1.Price>> GetPrice(Guid id)
        {
            var price = await _bll.Prices.PublicFirstOrDefaultAsync(id);

            if (price == null)
            {
                return NotFound();
            }

            return price;
        }

        // PUT: api/Prices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Change Price entity by its id
        /// </summary>
        /// <param name="id">Price entity id that need to be changed</param>
        /// <param name="price">Price entity that need to be changed</param>
        /// <returns>Result</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPrice(Guid id, App.Public.DTO.v1.Price price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            _bll.Prices.Update(price);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PriceExists(id))
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

        // POST: api/Prices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post new Price entity and save to the database
        /// </summary>
        /// <param name="price">Provide current price, price date, stock id, created at</param>
        /// <returns>Newly created price entity</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Price), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<App.Public.DTO.v1.Price>> PostPrice(App.Public.DTO.v1.Price price)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            _bll.Prices.Add(price);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new
            {
                id = price.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, price);
        }

        // DELETE: api/Prices/5
        /// <summary>
        /// Delete Price entity from database by id
        /// </summary>
        /// <param name="id">Price entity id that need to be deleted</param>
        /// <returns><Result/returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePrice(Guid id)
        {
            await _bll.Prices.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> PriceExists(Guid id)
        {
            return await _bll.Prices.ExistsAsync(id);
        }
    }
}
