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
    public class StocksController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public StocksController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/Stocks
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Stocks.GetAllAsync(); 
            return View(res);
        }

        // GET: Admin/Stocks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _uow.Stocks.FirstOrDefaultAsync(id.Value);
            
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Admin/Stocks/Create
        public IActionResult Create()
        {
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
                _uow.Stocks.Add(stock);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(stock);
        }

        // GET: Admin/Stocks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _uow.Stocks.FirstOrDefaultAsync(id.Value);
            if (stock == null)
            {
                return NotFound();
            }
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
                    _uow.Stocks.Update(stock);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await StockExists(stock.Id))
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
            return View(stock);
        }

        // GET: Admin/Stocks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _uow.Stocks
                .FirstOrDefaultAsync(id.Value);
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
            await _uow.Stocks.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StockExists(Guid id)
        {
            return await _uow.Stocks.ExistsAsync(id);
        }
    }
}
