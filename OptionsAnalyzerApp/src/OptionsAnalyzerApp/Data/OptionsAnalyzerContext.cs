using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OptionsAnalyzerApp.Models;

namespace OptionsAnalyzerApp.Data
{
    public class OptionsAnalyzerContext : DbContext
    {
        public OptionsAnalyzerContext(DbContextOptions<OptionsAnalyzerContext> options) : base(options)
        {
        }

        public DbSet<TradingAccount> TradingAccounts { get; set; }
        public DbSet<TradingAccountPriceChangeTarget> TradingAccountPriceChangeTargets { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionPriceChangeTarget> OptionPriceChangeTargets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TradingAccountPriceChangeTarget>()
                    .HasOne(g => g.TradingAccount)
                    .WithMany(t => t.PriceChangeTargets)
                    .HasForeignKey(g => g.TradingAccountID)
                    .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
        }
    }
}
