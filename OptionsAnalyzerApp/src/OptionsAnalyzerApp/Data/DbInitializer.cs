using OptionsAnalyzerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsAnalyzerApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(OptionsAnalyzerContext context)
        {
            context.Database.EnsureCreated();

            if(context.TradingAccounts.Count() == 0)
            {
                TradingAccount paperMoney = new TradingAccount { Name = "paperMoney", Balance = 69957.25m, UnitSize = .5m, RoundTripCommission = 1.5m, ExpectedPriceChange = .4m };

                context.TradingAccounts.Add(paperMoney);

                context.SaveChanges();
            }
        }
    }
}
