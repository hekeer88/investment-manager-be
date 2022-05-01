#nullable disable

using App.Contracts.BLL;
using App.DAL.EF;
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
    public class PortfoliosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public PortfoliosController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Portfolios
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.BLL.DTO.Portfolio>), 200)]
        [AllowAnonymous]
        [HttpGet]
        // TODO> ProducesRepsponse, meeteodi return jne peaks olema vist DTO.BLL.portfolio
        public async Task<IEnumerable<App.BLL.DTO.Portfolio>> GetPortfolios()
        {
            return await _bll.Portfolios.GetAllAsync();
            
        }

        // GET: api/Portfolios/5
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Portfolio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(Guid id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);

            if (portfolio == null)
            {
                return NotFound();
            }

            return portfolio;
        }

        // PUT: api/Portfolios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortfolio(Guid id, Portfolio portfolio)
        {
            if (id != portfolio.Id)
            {
                return BadRequest();
            }

            _context.Entry(portfolio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioExists(id))
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
        public async Task<ActionResult<Portfolio>> PostPortfolio([FromBody] Portfolio portfolio)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            
            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();

            
            // TODO: change return value for every post return like here, addition to id, add version,
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
        public async Task<IActionResult> DeletePortfolio(Guid id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PortfolioExists(Guid id)
        {
            return _context.Portfolios.Any(e => e.Id == id);
        }
    }
}
