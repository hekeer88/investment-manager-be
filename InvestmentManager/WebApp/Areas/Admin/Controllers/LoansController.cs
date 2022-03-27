#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using App.DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoansController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public LoansController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/Loans
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Loans.GetAllAsync(); 
            return View(res);
        }

        // GET: Admin/Loans/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _uow.Loans.FirstOrDefaultAsync(id.Value);
            
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Admin/Loans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanName,BorrowerName,ContractNumber,Collateral,LoanDate,EndDate,Amount,ScheduleType,Interest,PortfolioId,RegionId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.Id = Guid.NewGuid();
                _uow.Loans.Add(loan);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loan);
        }

        // GET: Admin/Loans/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _uow.Loans.FirstOrDefaultAsync(id.Value);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        // POST: Admin/Loans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LoanName,BorrowerName,ContractNumber,Collateral,LoanDate,EndDate,Amount,ScheduleType,Interest,PortfolioId,RegionId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Loan loan)
        {
            if (id != loan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Loans.Update(loan);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LoanExists(loan.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loan);
        }

        // GET: Admin/Loans/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _uow.Loans
                .FirstOrDefaultAsync(id.Value);
 
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Admin/Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Loans.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LoanExists(Guid id)
        {
            return await _uow.Loans.ExistsAsync(id);
        }
    }
}
