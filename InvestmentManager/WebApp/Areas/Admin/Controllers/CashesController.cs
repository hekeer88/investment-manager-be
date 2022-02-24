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
    public class CashesController : Controller
    {
        private readonly AppDbContext _context;

        public CashesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Cashes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Cashes.Include(c => c.Portfolio);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Cashes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cash = await _context.Cashes
                .Include(c => c.Portfolio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cash == null)
            {
                return NotFound();
            }

            return View(cash);
        }

        // GET: Admin/Cashes/Create
        public IActionResult Create()
        {
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name");
            return View();
        }

        // POST: Admin/Cashes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Currency,PortfolioId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Cash cash)
        {
            if (ModelState.IsValid)
            {
                cash.Id = Guid.NewGuid();
                _context.Add(cash);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name", cash.PortfolioId);
            return View(cash);
        }

        // GET: Admin/Cashes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cash = await _context.Cashes.FindAsync(id);
            if (cash == null)
            {
                return NotFound();
            }
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name", cash.PortfolioId);
            return View(cash);
        }

        // POST: Admin/Cashes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Currency,PortfolioId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Cash cash)
        {
            if (id != cash.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cash);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashExists(cash.Id))
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
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name", cash.PortfolioId);
            return View(cash);
        }

        // GET: Admin/Cashes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cash = await _context.Cashes
                .Include(c => c.Portfolio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cash == null)
            {
                return NotFound();
            }

            return View(cash);
        }

        // POST: Admin/Cashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cash = await _context.Cashes.FindAsync(id);
            _context.Cashes.Remove(cash);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashExists(Guid id)
        {
            return _context.Cashes.Any(e => e.Id == id);
        }
    }
}
