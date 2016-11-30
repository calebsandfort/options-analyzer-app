using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptionsAnalyzerApp.Models;
using OptionsAnalyzerApp.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OptionsAnalyzerApp.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly OptionsAnalyzerContext _context;

        public MaintenanceController(OptionsAnalyzerContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UpdateOptions()
        {
            Option firstOption = _context.Options.First();

            UpdateOptionsModel updateOptionsModel = new UpdateOptionsModel { Expiry = firstOption.Expiry, UnderlyingPrice = firstOption.UnderlyingPrice };

            return View(updateOptionsModel);
        }

        // POST: TradingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOptions([Bind("Expiry,UnderlyingPrice")] UpdateOptionsModel updateOptionsModel)
        {
            if (ModelState.IsValid)
            {
                TradingAccount tradingAccount = _context.TradingAccounts.First();
                Decimal spread;

                foreach (Option option in await _context.Options.ToListAsync())
                {
                    option.Expiry = updateOptionsModel.Expiry;
                    option.UnderlyingPrice = updateOptionsModel.UnderlyingPrice;
                    option.BlackScholesPrice = option.CalculateBlackScholesPrice(tradingAccount, updateOptionsModel.UnderlyingPrice);

                    spread = option.Spread;
                    option.Bid = option.BlackScholesPrice - (spread / 2m);
                    option.Ask = option.BlackScholesPrice + (spread / 2m);
                    option.FillCalculatedFields(tradingAccount);
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(updateOptionsModel);
        }
    }
}
