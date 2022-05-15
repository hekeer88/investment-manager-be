#nullable disable

using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LoansController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public LoansController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<IEnumerable<App.BLL.DTO.Loan>> GetLoans()
        {
            return await _bll.Loans.GetAllAsync();
        }

        // GET: api/Loans/5
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Loan>), 200)]
        [AllowAnonymous]
        [HttpGet("{portfolioId}")]
        public async Task<IEnumerable<App.Public.DTO.v1.Loan>> GetLoans(Guid portfolioId)
        {
            return await _bll.Loans.GetAllPublicAsync(portfolioId);
        }
        
        // GET: api/Loans/5
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Loan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Loan>> GetLoan(Guid id)
        {

            var loan = await _bll.Loans.FirstOrDefaultPublicAsync(id);
            
            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(Guid id, App.BLL.DTO.Loan loan)
        {
            if (id != loan.Id)
            {
                return BadRequest();
            }

            _bll.Loans.Add(loan);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LoanExists(id))
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

        // POST: api/Loans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Portfolio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<App.Public.DTO.v1.Loan>> PostLoan([FromBody] App.Public.DTO.v1.Loan loan)
        {

            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            
            _bll.Loans.Add(loan);
            await _bll.SaveChangesAsync();
            
            return CreatedAtAction(
                "GetLoan",
                new
                {
                    id = loan.Id,
                    version = HttpContext.GetRequestedApiVersion()!.ToString()
                }
                , loan);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(Guid id)
        {
            await _bll.Loans.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> LoanExists(Guid id)
        {
            return await _bll.Loans.ExistsAsync(id);
        }
    }
}
