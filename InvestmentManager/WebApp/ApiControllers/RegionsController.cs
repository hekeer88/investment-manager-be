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
using WebApp.DTO;
using Industry = App.Public.DTO.v1.Industry;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RegionsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public RegionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Regions
        /// <summary>
        /// Get all Region entites
        /// </summary>
        /// <returns>All Region entities</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Region>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Region>> GetRegions()
        {
            return await _bll.Regions.PublicGetAllAsync(User.GetUserId());
        }

        // GET: api/Regions/5
        /// <summary>
        /// Get Region entity by id
        /// </summary>
        /// <param name="id">Region entity id</param>
        /// <returns>Region entity</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Region), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.Public.DTO.v1.Region>> GetRegion(Guid id)
        {
            var region = await _bll.Regions.PublicFirstOrDefaultAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return region;
        }

        // PUT: api/Regions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Change Region entity by id
        /// </summary>
        /// <param name="id">Region entity id that need to be changed</param>
        /// <param name="region">Region entity that needs to be changed</param>
        /// <returns>Result</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutRegion(Guid id, Industry region)
        {
            if (id != region.Id)
            {
                return BadRequest();
            }
            
            _bll.Industries.Update(region);
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RegionExists(id))
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

        // POST: api/Regions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post new Region entity to the database
        /// </summary>
        /// <param name="region">Provide country, continent</param>
        /// <returns>Newly created Region entity</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Region), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<App.Public.DTO.v1.Region>> PostRegion(
            [FromBody ]App.Public.DTO.v1.Region region)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            
            _bll.Regions.Add(region);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetRegion", new
                {
                    id = region.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }, region);
        }
        

        // DELETE: api/Regions/5
        /// <summary>
        /// Delete Region entity from database by id
        /// </summary>
        /// <param name="id">Region entity id that needs to be deleted</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            await _bll.Regions.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> RegionExists(Guid id)
        {
            return await _bll.Regions.ExistsAsync(id);
        }
    }
}
