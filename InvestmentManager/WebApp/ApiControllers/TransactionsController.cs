#nullable disable

using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Base.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TransactionsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public TransactionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Transactions
        /// <summary>
        /// Get all Transaction entities related with Stock entity and specific user
        /// </summary>
        /// <returns>All Transaction entities related with Stock entity and specific user</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IEnumerable<App.Public.DTO.v1.Transaction>), 200)]
        public async Task<IEnumerable<App.Public.DTO.v1.Transaction>> GetTransactions()
        {
            return await _bll.Transactions.PublicGetAllAsync(User.GetUserId());
        }

        // GET: api/Transactions/5
        /// <summary>
        /// Get Transaction entity by its id 
        /// </summary>
        /// <param name="id">Transaction entity id</param>
        /// <returns>Transaction entity</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Transaction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<App.Public.DTO.v1.Transaction>> GetTransaction(Guid id)
        {
            var transaction = await _bll.Transactions.PublicFirstOrDefaultAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Change Transaction entity by its id
        /// </summary>
        /// <param name="id">Transaction entity id that needs to be changed</param>
        /// <param name="transaction">Transaction entity that needs to be </param>
        /// <returns>Transaction entity</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTransaction(Guid id, App.Public.DTO.v1.Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            _bll.Transactions.Update(transaction);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Post new Transaction entity
        /// </summary>
        /// <param name="transaction">Provide quantity, price, date, type, stockId</param>
        /// <returns>Newly create Transaction entity</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(App.Public.DTO.v1.Transaction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<App.Public.DTO.v1.Transaction>> PostTransaction(App.Public.DTO.v1.Transaction transaction)
        {
            if (HttpContext.GetRequestedApiVersion() == null)
            {
                return BadRequest("Api version is mandatory");
            }
            _bll.Transactions.Add(transaction);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetTransaction", new
            {
                id = transaction.Id,
                version = HttpContext.GetRequestedApiVersion()!.ToString()
            }, transaction);
        }

        // DELETE: api/Transactions/5
        /// <summary>
        /// Delete Transaction entity by its id
        /// </summary>
        /// <param name="id">Transaction entity id that needs to be deleted</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            await _bll.Transactions.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> TransactionExists(Guid id)
        {
            return await _bll.Transactions.ExistsAsync(id);
        }
    }
}
