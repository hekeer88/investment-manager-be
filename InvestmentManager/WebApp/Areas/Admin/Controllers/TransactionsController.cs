#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransactionsController : Controller
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Transactions
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Transactions.Include(t => t.Cash).Include(t => t.Loan).Include(t => t.Stock);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Transactions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Cash)
                .Include(t => t.Loan)
                .Include(t => t.Stock)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Admin/Transactions/Create
        public IActionResult Create()
        {
            ViewData["CashId"] = new SelectList(_context.Cashes, "Id", "Currency");
            ViewData["LoanId"] = new SelectList(_context.Loans, "Id", "BorrowerName");
            ViewData["StockId"] = new SelectList(_context.Stocks, "Id", "Company");
            return View();
        }

        // POST: Admin/Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,TransactionPrice,TransactionDate,Type,StockId,LoanId,CashId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Id = Guid.NewGuid();
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CashId"] = new SelectList(_context.Cashes, "Id", "Currency", transaction.CashId);
            ViewData["LoanId"] = new SelectList(_context.Loans, "Id", "BorrowerName", transaction.LoanId);
            ViewData["StockId"] = new SelectList(_context.Stocks, "Id", "Company", transaction.StockId);
            return View(transaction);
        }

        // GET: Admin/Transactions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["CashId"] = new SelectList(_context.Cashes, "Id", "Currency", transaction.CashId);
            ViewData["LoanId"] = new SelectList(_context.Loans, "Id", "BorrowerName", transaction.LoanId);
            ViewData["StockId"] = new SelectList(_context.Stocks, "Id", "Company", transaction.StockId);
            return View(transaction);
        }

        // POST: Admin/Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,TransactionPrice,TransactionDate,Type,StockId,LoanId,CashId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            ViewData["CashId"] = new SelectList(_context.Cashes, "Id", "Currency", transaction.CashId);
            ViewData["LoanId"] = new SelectList(_context.Loans, "Id", "BorrowerName", transaction.LoanId);
            ViewData["StockId"] = new SelectList(_context.Stocks, "Id", "Company", transaction.StockId);
            return View(transaction);
        }

        // GET: Admin/Transactions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Cash)
                .Include(t => t.Loan)
                .Include(t => t.Stock)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Admin/Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(Guid id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
