using Common.Models.WitnessReport;
using DataAccess.EF.Context;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class WitnessReportRepository : IWitnessReportRepository
    {
        private readonly WitnessReportDbContext dbContext;

        public WitnessReportRepository(WitnessReportDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<WitnessReport> GetAll()
        {
            return this.dbContext.WitnessReports.AsNoTracking().ToList();
        }

        public IList<WitnessReport> GetByCaseName(string caseName)
        {
            return this.dbContext.WitnessReports.Where(x => x.CaseName == caseName).AsNoTracking().ToList();
        }

        public void Save(WitnessReport witnessReport)
        {
            this.dbContext.WitnessReports.Add(witnessReport);
            this.dbContext.SaveChanges();
        }

        public IDbContextTransaction OpenTransaction()
        {
            return this.dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            this.dbContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            this.dbContext.Database.RollbackTransaction();
        }
    }
}
