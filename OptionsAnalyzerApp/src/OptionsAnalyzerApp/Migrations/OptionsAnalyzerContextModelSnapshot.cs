﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OptionsAnalyzerApp.Data;

namespace OptionsAnalyzerApp.Migrations
{
    [DbContext(typeof(OptionsAnalyzerContext))]
    partial class OptionsAnalyzerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("Contracts");

                    b.Property<decimal>("Delta");

                    b.Property<decimal>("Strike");

                    b.HasKey("ID");

                    b.ToTable("Options");
                });
        }
    }
}