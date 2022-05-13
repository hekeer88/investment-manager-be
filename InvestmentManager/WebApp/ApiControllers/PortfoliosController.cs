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
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Portfolio>), 200)]
        [AllowAnonymous]
        [HttpGet]
        // TODO: return public.dto.portfolio?
        public async Task<IEnumerable<App.Public.DTO.v1.Portfolio>> GetPortfolios()
        {
            return await _bll.Portfolios.GetAllAsyncPublic(User.GetUserId());
        }

        // GET: api/Portfolios/5
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.BLL.DTO.Portfolio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        // TODO: ka public dto
        public async Task<ActionResult<App.BLL.DTO.Portfolio>> GetPortfolio(Guid id)
        {
            var portfolio = await _bll.Portfolios.FirstOrDefaultAsync(id);

            if (portfolio == null)
            {
                return NotFound();
            }

            return portfolio;
        }

        // PUT: api/Portfolios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        // TODO: also DTO
        public async Task<IActionResult> PutPortfolio(Guid id, App.BLL.DTO.Portfolio portfolio)
        {
            if (id != portfolio.Id)
            {
                return BadRequest();
            }

            _bll.Portfolios.Add(portfolio);

            // _context.Entry(portfolio).State = EntityState.Modified;

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
        [AllowAnonymous]
        // TODO: from body is questionable, and olso Portfolia is domain.portfolio but should use some DTO
        public async Task<ActionResult<App.BLL.DTO.Portfolio>> PostPortfolio([FromBody] Portfolio portfolio)
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
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.BLL.DTO.Portfolio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpDelete("{id}")]
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
