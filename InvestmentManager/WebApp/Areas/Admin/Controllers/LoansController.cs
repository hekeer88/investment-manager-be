#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using WebApp.Data;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoansController : Controller
    {
        private readonly AppDbContext _context;

        public LoansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Loans
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Loans.Include(l => l.Portfolio).Include(l => l.Region);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Loans/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Portfolio)
                .Include(l => l.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Admin/Loans/Create
        public IActionResult Create()
        {
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name");
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Continent");
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
                _context.Add(loan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name", loan.PortfolioId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Continent", loan.RegionId);
            return View(loan);
        }

        // GET: Admin/Loans/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name", loan.PortfolioId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Continent", loan.RegionId);
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
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.Id))
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
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name", loan.PortfolioId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Continent", loan.RegionId);
            return View(loan);
        }

        // GET: Admin/Loans/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Portfolio)
                .Include(l => l.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var loan = await _context.Loans.FindAsync(id);
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(Guid id)
        {
            return _context.Loans.Any(e => e.Id == id);
        }
    }
}
