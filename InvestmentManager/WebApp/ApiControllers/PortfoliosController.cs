#nullable disable

using App.Contracts.BLL;
using App.DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Portfolio = App.BLL.DTO.Portfolio;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PortfoliosController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PortfoliosController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Portfolios
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Portfolio>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Portfolio>> GetPortfolios()
        {
            return await _bll.Portfolios.PublicGetAllAsync(User.GetUserId());
        }

        // GET: api/Portfolios/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Portfolio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.Public.DTO.v1.Portfolio>> GetPortfolio(Guid id)
        {

            var portfolio = await _bll.Portfolios.PublicFirstOrDefaultAsync(id);
            
            if (portfolio == null)
            {
                return NotFound();
            }

            return portfolio;
        }

        // PUT: api/Portfolios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPortfolio(Guid id, App.Public.DTO.v1.Portfolio portfolio)
        {
            if (id != portfolio.Id)
            {
                return BadRequest();
            }

            portfolio.AppUserId = User.GetUserId();
            _bll.Portfolios.Update(portfolio);


            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PortfolioExists(id))
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

        // POST: api/Portfolios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Portfolio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<App.Public.DTO.v1.Portfolio>> PostPortfolio([FromBody] App.Public.DTO.v1.Portfolio portfolio)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            
            portfolio.AppUserId = User.GetUserId();
            _bll.Portfolios.Add(portfolio);
            await _bll.SaveChangesAsync();

            
            // TODO: for swagger change return value for every post return like here,
            // addition to id, add version,
            // also add this if control in the beginning of this method
            return CreatedAtAction(
                "GetPortfolio",
                new
                {
                    id = portfolio.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                },
                portfolio);
        }

        // DELETE: api/Portfolios/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePortfolio(Guid id)
        {
            await _bll.Portfolios.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> PortfolioExists(Guid id)
        {
            return await _bll.Portfolios.ExistsAsync(id);
        }
    }
}
