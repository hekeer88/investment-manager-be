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

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class IndustriesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public IndustriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Industries
        /// <summary>
        /// Get all Industry entities that are saved in the Database.
        /// </summary>
        /// <returns>All Industry entites</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Industry>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Industry>> GetIndustries()
        {
            return await _bll.Industries.PublicGetAllAsync(User.GetUserId());
        }

        // GET: api/Industries/5
        /// <summary>
        /// Get Industry entity by id
        /// </summary>
        /// <param name="id">Provide industry id</param>
        /// <returns>Industry entity</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Industry), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.Public.DTO.v1.Industry>> GetIndustry(Guid id)
        {
            var industry = await _bll.Industries.PublicFirstOrDefaultAsync(id);

            if (industry == null)
            {
                return NotFound();
            }

            return industry;
        }

        // PUT: api/Industries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Change Industry entity
        /// </summary>
        /// <param name="id">Industry id and Industry entity that need to be changed</param>
        /// <param name="industry"></param>
        /// <returns>Result</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutIndustry(Guid id,  App.Public.DTO.v1.Industry  industry)
        {
            if (id != industry.Id)
            {
                return BadRequest();
            }
            
            _bll.Industries.Update(industry);
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await IndustryExists(id))
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

        // POST: api/Industries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create new Industry entity and save to the database
        /// </summary>
        /// <param name="industry">Provice: Industry name, created by, created at</param>
        /// <returns>New created Industry Entity</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Portfolio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<App.Public.DTO.v1.Industry>> PostIndustry([FromBody] App.Public.DTO.v1.Industry industry)
        {
            
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            
            _bll.Industries.Add(industry);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetIndustry", new
                {
                    id = industry.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }, industry);
        }

        // DELETE: api/Industries/5
        /// <summary>
        /// Deletes Industry entity by id
        /// </summary>
        /// <param name="id">Provide Industry id that needd to be deleted</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteIndustry(Guid id)
        {
            await _bll.Industries.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> IndustryExists(Guid id)
        {
            return await _bll.Industries.ExistsAsync(id);
        }
    }
}
