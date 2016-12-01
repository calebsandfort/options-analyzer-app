using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OptionsAnalyzerApp.Models;
using OptionsAnalyzerApp.Data;
using Microsoft.EntityFrameworkCore;
using OptionsAnalyzerApp.Framework;

namespace OptionsAnalyzerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly OptionsAnalyzerContext _context;

        public HomeController(OptionsAnalyzerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(String sortProperty, String sortDirection, Decimal? expectedPriceChange)
        {
            OptionsLayout optionsLayout = new OptionsLayout();

            TradingAccount tradingAccount = await _context.TradingAccounts.FirstOrDefaultAsync();

            if (expectedPriceChange.HasValue)
            {
                tradingAccount.ExpectedPriceChange = expectedPriceChange.Value;

                foreach(Option option in await _context.Options.ToListAsync())
                {
                    option.FillCalculatedFields(tradingAccount);
                }

                await _context.SaveChangesAsync();
            }

            optionsLayout.TradingAccount = tradingAccount;

            if (String.IsNullOrEmpty(sortProperty))
            {
                sortProperty = "BlackScholesPLPercent";
            }

            if (String.IsNullOrEmpty(sortDirection))
            {
                sortDirection = "desc";
            }

            ViewData["sortProperty"] = sortProperty;
            ViewData["sortDirection"] = sortDirection;

            optionsLayout.Puts = _context.Options.Where(x => x.OptionType == OptionTypes.Put && x.BlackScholesPLPercent > 0).OrderBy(sortProperty, sortDirection).ToList();
            optionsLayout.Calls = _context.Options.Where(x => x.OptionType == OptionTypes.Call && x.BlackScholesPLPercent > 0).OrderBy(sortProperty, sortDirection).ToList();

            return View(optionsLayout);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
