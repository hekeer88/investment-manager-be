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
        private readonly IPortfolioRepository _repo;

        
        // changes for using REPO
        public PortfoliosController(IPortfolioRepository repo)
        {
            _repo = repo;
        }

        // changes for using REPO
        // GET: Admin/Portfolios
        public async Task<IActionResult> Index()
        {
            var res = await _repo.GetAllAsync(); 
            return View(res);
        }

        // changes for using REPO
        // GET: Admin/Portfolios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _repo.FirstOrDefaultAsync(id.Value);
            
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // changes for using REPO
        // GET: Admin/Portfolios/Create
        public IActionResult Create()
        {
            return View();
        }

        // REPO CHANGES
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
                _repo.Add(portfolio);

                await _repo.SaveChangesAsync();
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

            var portfolio = await _repo.FirstOrDefaultAsync(id.Value);
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
                    _repo.Update(portfolio);
                    await _repo.SaveChangesAsync();
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

            var portfolio = await _repo
                .FirstOrDefaultAsync(id.Value);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // REPO
        // POST: Admin/Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
       
            await _repo.RemoveAsync(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PortfolioExists(Guid id)
        {
            return await _repo.ExistsAsync(id);
        }
    }
}
