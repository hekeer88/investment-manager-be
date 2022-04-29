#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using Stock = App.BLL.DTO.Stock;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StocksController : Controller
    {
        private readonly IAppBLL _bll;

        public StocksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Admin/Stocks
        public async Task<IActionResult> Index()
        {
            var res = await _bll.Stocks.GetAllAsync(); 
            return View(res);
        }

        // GET: Admin/Stocks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _bll.Stocks.FirstOrDefaultAsync(id.Value);
            
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
                _bll.Stocks.Add(stock);
                await _bll.SaveChangesAsync();
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

            var stock = await _bll.Stocks.FirstOrDefaultAsync(id.Value);
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
                    _bll.Stocks.Update(stock);
                    await _bll.SaveChangesAsync();
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

            var stock = await _bll.Stocks
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
            await _bll.Stocks.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> StockExists(Guid id)
        {
            return await _bll.Stocks.ExistsAsync(id);
        }
    }
}
