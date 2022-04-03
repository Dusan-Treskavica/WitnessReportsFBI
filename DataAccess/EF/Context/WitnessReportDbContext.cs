using Common.Models.WitnessReport;
using DataAccess.EF.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.EF.Context
{
    public class WitnessReportDbContext : DbContext
    {
        public WitnessReportDbContext(DbContextOptions<WitnessReportDbContext> options) : base(options)
        { }

        public DbSet<WitnessReport> WitnessReports { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WitnessReportConfiguration());
        }
    }
}
