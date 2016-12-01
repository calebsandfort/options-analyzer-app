using Microsoft.AspNetCore.Mvc;
using OptionsAnalyzerApp.Data;
using OptionsAnalyzerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptionsAnalyzerApp.Framework;

namespace OptionsAnalyzerApp.ViewComponents
{
    public class OptionsTableViewComponent : ViewComponent
    {
        private readonly OptionsAnalyzerContext _context;

        public OptionsTableViewComponent(OptionsAnalyzerContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(OptionTypes optionType)
        {
            List<Option> options = await _context.Options.Where(x => x.OptionType == optionType && x.BlackScholesPLPercent > 0).OrderBy(ViewData["sortProperty"].ToString(), ViewData["sortDirection"].ToString()).ToListAsync();
            return View("OptionsTable", options);
        }
    }
}
