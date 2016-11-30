using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OptionsAnalyzerApp.Data;

namespace OptionsAnalyzerApp.Migrations
{
    [DbContext(typeof(OptionsAnalyzerContext))]
    [Migration("20161129211025_AddRiskFreeInterestRate3")]
    partial class AddRiskFreeInterestRate3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OptionsAnalyzerApp.Models.Option", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Ask");

                    b.Property<decimal>("Bid");

                    b.Property<decimal>("Delta");

                    b.Property<decimal>("DeltaPL");

                    b.Property<decimal>("DeltaPLPercent");

                    b.Property<DateTime>("Expiry");

                    b.Property<decimal>("ImpliedVolatility");

                    b.Property<int>("OptionType");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("Strike");

                    b.Property<decimal>("UnderlyingPrice");

                    b.HasKey("ID");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("OptionsAnalyzerApp.Models.TradingAccount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.Property<decimal>("ExpectedPriceChange");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<decimal>("RiskFreeInterestRate")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("RoundTripCommission");

                    b.Property<decimal>("UnitSize");

                    b.HasKey("ID");

                    b.ToTable("TradingAccounts");
                });
        }
    }
}
