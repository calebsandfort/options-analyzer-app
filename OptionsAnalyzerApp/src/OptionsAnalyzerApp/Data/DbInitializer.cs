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
                TradingAccount paperMoney = new TradingAccount { Name = "paperMoney", Balance = 69957.25m, UnitSize = .5m, PriceChangeTargets = new List<TradingAccountPriceChangeTarget>() };
                paperMoney.PriceChangeTargets.Add(new TradingAccountPriceChangeTarget { PriceChangeTarget = .15m });
                paperMoney.PriceChangeTargets.Add(new TradingAccountPriceChangeTarget { PriceChangeTarget = .30m });
                paperMoney.PriceChangeTargets.Add(new TradingAccountPriceChangeTarget { PriceChangeTarget = .45m });
                paperMoney.PriceChangeTargets.Add(new TradingAccountPriceChangeTarget { PriceChangeTarget = .60m });
                paperMoney.PriceChangeTargets.Add(new TradingAccountPriceChangeTarget { PriceChangeTarget = .75m });

                context.TradingAccounts.Add(paperMoney);

                context.SaveChanges();
            }
        }
    }
}
