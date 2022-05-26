#nullable disable

using App.Contracts.BLL;
using Base.Extensions;
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
        /// <summary>
        /// Get all Loan entities by user.
        /// </summary>
        /// <returns>Returns all Loan entities by related user</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Loan>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Loan>> GetLoans()
        {
            return await _bll.Loans.PublicGetAllAsync(User.GetUserId());
            // return await _bll.Loans.GetAll();
        }


        // GET: api/Loans/5
        /// <summary>
        /// Get Loan entity by id
        /// </summary>
        /// <param name="id">Loan entity id</param>
        /// <returns>Loan entity by id</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Loan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.Public.DTO.v1.Loan>> GetLoan(Guid id)
        {

            var loan = await _bll.Loans.PublicFirstOrDefaultAsync(id);
            
            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Change Loan entity by id
        /// </summary>
        /// <param name="id">Loan entity id that need to be changed</param>
        /// <param name="loan">Loan entity that need to be changed</param>
        /// <returns>Result</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutLoan(Guid id, App.Public.DTO.v1.Loan loan)
        {
            if (id != loan.Id)
            {
                return BadRequest();
            }

            _bll.Loans.Update(loan);

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
        /// <summary>
        /// Post new Loan entity in database
        /// </summary>
        /// <param name="loan">Provide Loan name, borrower name, contract number, collateral(yes/no), loan date,
        /// due date, amount, schedule type, interest and portfolio id</param>
        /// <returns>Newly created Loan entity</returns>
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
        /// <summary>
        /// Delete entity by id
        /// </summary>
        /// <param name="id">Loan entity id that needs to be deleted</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
