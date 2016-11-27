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

        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
