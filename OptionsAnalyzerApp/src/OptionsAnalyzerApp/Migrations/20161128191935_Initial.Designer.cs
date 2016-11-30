using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OptionsAnalyzerApp.Data;

namespace OptionsAnalyzerApp.Migrations
{
    [DbContext(typeof(OptionsAnalyzerContext))]
    [Migration("20161128191935_Initial")]
    partial class Initial
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

                    b.Property<int?>("OptionID");

                    b.Property<int>("OptionType");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("Strike");

                    b.HasKey("ID");

                    b.HasIndex("OptionID");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("OptionsAnalyzerApp.Models.OptionPriceChangeTarget", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OptionID");

                    b.Property<decimal>("PL");

                    b.Property<decimal>("PLPercent");

                    b.Property<decimal>("PriceChangeTarget");

                    b.HasKey("ID");

                    b.HasIndex("OptionID");

                    b.ToTable("OptionPriceChangeTargets");
                });

            modelBuilder.Entity("OptionsAnalyzerApp.Models.TradingAccount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<decimal>("UnitSize");

                    b.HasKey("ID");

                    b.ToTable("TradingAccounts");
                });

            modelBuilder.Entity("OptionsAnalyzerApp.Models.TradingAccountPriceChangeTarget", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("PriceChangeTarget");

                    b.Property<int>("TradingAccountID");

                    b.HasKey("ID");

                    b.HasIndex("TradingAccountID");

                    b.ToTable("TradingAccountPriceChangeTargets");
                });

            modelBuilder.Entity("OptionsAnalyzerApp.Models.Option", b =>
                {
                    b.HasOne("OptionsAnalyzerApp.Models.Option")
                        .WithMany("Options")
                        .HasForeignKey("OptionID");
                });

            modelBuilder.Entity("OptionsAnalyzerApp.Models.OptionPriceChangeTarget", b =>
                {
                    b.HasOne("OptionsAnalyzerApp.Models.Option", "Option")
                        .WithMany()
                        .HasForeignKey("OptionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OptionsAnalyzerApp.Models.TradingAccountPriceChangeTarget", b =>
                {
                    b.HasOne("OptionsAnalyzerApp.Models.TradingAccount", "TradingAccount")
                        .WithMany("PriceChangeTargets")
                        .HasForeignKey("TradingAccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
