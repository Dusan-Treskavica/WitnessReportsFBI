using Common.Models.WitnessReport;
using DataAccess.EF.Context;
using DataAccess.Interface.Repository;
using Microsoft.EntityFrameworkCore;
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

        public void Insert(WitnessReport witnessReport)
        {
            this.dbContext.WitnessReports.Add(witnessReport);
        }
    }
}
