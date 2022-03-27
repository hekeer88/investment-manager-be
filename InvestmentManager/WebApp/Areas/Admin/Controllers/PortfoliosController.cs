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
    public class PortfoliosController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        
        public PortfoliosController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }
        
        // GET: Admin/Portfolios
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Portfolios.GetAllAsync(); 
            return View(res);
        }
        
        // GET: Admin/Portfolios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _uow.Portfolios.FirstOrDefaultAsync(id.Value);
            
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }
        
        // GET: Admin/Portfolios/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Admin/Portfolios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,AppUserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                portfolio.Id = Guid.NewGuid();
                _uow.Portfolios.Add(portfolio);

                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolio);
        }

        // GET: Admin/Portfolios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _uow.Portfolios.FirstOrDefaultAsync(id.Value);
            if (portfolio == null)
            {
                return NotFound();
            }
            return View(portfolio);
        }

        // POST: Admin/Portfolios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,AppUserId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Portfolio portfolio)
        {
            if (id != portfolio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Portfolios.Update(portfolio);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PortfolioExists(portfolio.Id))
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
            return View(portfolio);
        }

        // GET: Admin/Portfolios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _uow.Portfolios
                .FirstOrDefaultAsync(id.Value);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }
        
        // POST: Admin/Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
       
            await _uow.Portfolios.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PortfolioExists(Guid id)
        {
            return await _uow.Portfolios.ExistsAsync(id);
        }
    }
}
