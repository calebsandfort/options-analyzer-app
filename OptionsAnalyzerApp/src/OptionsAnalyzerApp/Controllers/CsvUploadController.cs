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

        public IActionResult Create()
        {
            return View();
        }

        // POST: TradingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Contents")] CsvUpload csvUpload)
        {
            if (ModelState.IsValid)
            {
                _context.Options.Clear();

                using (TextReader tr = new StringReader(csvUpload.Contents.Replace(",,", String.Empty)))
                {
                    var csv = new CsvReader(tr, new CsvConfiguration() { HasHeaderRecord = true });
                    while (csv.Read())
                    {
                        String symbol = csv.GetField<String>(0);

                        Option callOption = new Option();
                        callOption.OptionType = OptionTypes.Call;
                        callOption.Delta = csv.GetField<Decimal>(0);
                        callOption.Bid = csv.GetField<Decimal>(4);
                        callOption.Ask = csv.GetField<Decimal>(6);
                        callOption.Strike = csv.GetField<int>(9);

                        _context.Options.Add(callOption);

                        Option putOption = new Option();
                        putOption.OptionType = OptionTypes.Put;
                        putOption.Delta = csv.GetField<Decimal>(14);
                        putOption.Bid = csv.GetField<Decimal>(10);
                        putOption.Ask = Math.Abs(csv.GetField<Decimal>(12));
                        putOption.Strike = csv.GetField<int>(9);

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
