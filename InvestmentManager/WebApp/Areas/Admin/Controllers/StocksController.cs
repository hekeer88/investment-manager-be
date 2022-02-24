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
    public class StocksController : Controller
    {
        private readonly AppDbContext _context;

        public StocksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Stocks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Stocks.Include(s => s.Industry).Include(s => s.Portfolio).Include(s => s.Region);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Stocks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Industry)
                .Include(s => s.Portfolio)
                .Include(s => s.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Admin/Stocks/Create
        public IActionResult Create()
        {
            ViewData["IndustryId"] = new SelectList(_context.Industries, "Id", "Name");
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name");
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Continent");
            return View();
        }

        // POST: Admin/Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Company,Ticker,Comment,RegionId,PortfolioId,IndustryId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                stock.Id = Guid.NewGuid();
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IndustryId"] = new SelectList(_context.Industries, "Id", "Name", stock.IndustryId);
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name", stock.PortfolioId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Continent", stock.RegionId);
            return View(stock);
        }

        // GET: Admin/Stocks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["IndustryId"] = new SelectList(_context.Industries, "Id", "Name", stock.IndustryId);
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name", stock.PortfolioId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Continent", stock.RegionId);
            return View(stock);
        }

        // POST: Admin/Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Company,Ticker,Comment,RegionId,PortfolioId,IndustryId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Stock stock)
        {
            if (id != stock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.Id))
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
            ViewData["IndustryId"] = new SelectList(_context.Industries, "Id", "Name", stock.IndustryId);
            ViewData["PortfolioId"] = new SelectList(_context.Portfolios, "Id", "Name", stock.PortfolioId);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Continent", stock.RegionId);
            return View(stock);
        }

        // GET: Admin/Stocks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Industry)
                .Include(s => s.Portfolio)
                .Include(s => s.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Admin/Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(Guid id)
        {
            return _context.Stocks.Any(e => e.Id == id);
        }
    }
}
