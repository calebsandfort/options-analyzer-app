using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OptionsAnalyzerApp.Models;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using OptionsAnalyzerApp.Framework;
using OptionsAnalyzerApp.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OptionsAnalyzerApp.Controllers
{
    public class CsvUploadController : Controller
    {
        private readonly OptionsAnalyzerContext _context;

        public CsvUploadController(OptionsAnalyzerContext context)
        {
            _context = context;
        }

        public IActionResult Upload()
        {
            return View();
        }

        // POST: TradingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload([Bind("Contents")] CsvUpload csvUpload)
        {
            if (ModelState.IsValid)
            {
                _context.Options.Clear();

                TradingAccount tradingAccount = _context.TradingAccounts.FirstOrDefault();

                String[] split = csvUpload.Contents.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Decimal underlyingPrice = Decimal.Parse(split[0]);

                using (TextReader tr = new StringReader(String.Join(Environment.NewLine, split.Skip(1)).Replace(",,", String.Empty).Replace("%", String.Empty)))
                {
                    var csv = new CsvReader(tr, new CsvConfiguration() { HasHeaderRecord = true });
                    while (csv.Read())
                    {
                        Option callOption = new Option();
                        callOption.OptionType = OptionTypes.Call;
                        callOption.Delta = csv.GetField<Decimal>(0);
                        callOption.ImpliedVolatility = csv.GetField<Decimal>(1) / 100m;
                        callOption.Bid = csv.GetField<Decimal>(2);
                        callOption.Ask = csv.GetField<Decimal>(5);
                        callOption.Expiry = csv.GetField<DateTime>(7);
                        callOption.Strike = csv.GetField<Decimal>(8);
                        callOption.UnderlyingPrice = underlyingPrice;

                        callOption.FillCalculatedFields(tradingAccount);

                        _context.Options.Add(callOption);

                        Option putOption = new Option();
                        putOption.OptionType = OptionTypes.Put;
                        putOption.Delta = Math.Abs(csv.GetField<Decimal>(13));
                        putOption.ImpliedVolatility = csv.GetField<Decimal>(14) / 100m;
                        putOption.Bid = csv.GetField<Decimal>(9);
                        putOption.Ask = csv.GetField<Decimal>(11);
                        putOption.Expiry = csv.GetField<DateTime>(7);
                        putOption.Strike = csv.GetField<Decimal>(8);
                        putOption.UnderlyingPrice = underlyingPrice;

                        putOption.FillCalculatedFields(tradingAccount);

                        _context.Options.Add(putOption);

                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Home");
            }
            return View(csvUpload);
        }
    }
}
