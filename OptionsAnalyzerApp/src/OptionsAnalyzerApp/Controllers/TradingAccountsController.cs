using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptionsAnalyzerApp.Data;
using OptionsAnalyzerApp.Models;

namespace OptionsAnalyzerApp.Controllers
{
    public class TradingAccountsController : Controller
    {
        private readonly OptionsAnalyzerContext _context;

        public TradingAccountsController(OptionsAnalyzerContext context)
        {
            _context = context;    
        }

        // GET: TradingAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TradingAccounts.OrderBy(x => x.Name).ToListAsync());
        }

        // GET: TradingAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tradingAccount = await _context.TradingAccounts.SingleOrDefaultAsync(m => m.ID == id);
            if (tradingAccount == null)
            {
                return NotFound();
            }

            return View(tradingAccount);
        }

        // GET: TradingAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TradingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Balance,Name,UnitSize")] TradingAccount tradingAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tradingAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tradingAccount);
        }

        // GET: TradingAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tradingAccount = await _context.TradingAccounts.SingleOrDefaultAsync(m => m.ID == id);
            if (tradingAccount == null)
            {
                return NotFound();
            }
            return View(tradingAccount);
        }

        // POST: TradingAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Balance,Name,UnitSize")] TradingAccount tradingAccount)
        {
            if (id != tradingAccount.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tradingAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TradingAccountExists(tradingAccount.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(tradingAccount);
        }

        // GET: TradingAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tradingAccount = await _context.TradingAccounts.SingleOrDefaultAsync(m => m.ID == id);
            if (tradingAccount == null)
            {
                return NotFound();
            }

            return View(tradingAccount);
        }

        // POST: TradingAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tradingAccount = await _context.TradingAccounts.SingleOrDefaultAsync(m => m.ID == id);
            _context.TradingAccounts.Remove(tradingAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TradingAccountExists(int id)
        {
            return _context.TradingAccounts.Any(e => e.ID == id);
        }
    }
}
