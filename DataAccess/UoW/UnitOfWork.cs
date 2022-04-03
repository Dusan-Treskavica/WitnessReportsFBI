using DataAccess.EF.Context;
using DataAccess.Interface.Repository;
using DataAccess.Interface.UoW;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WitnessReportDbContext dbContext;
        private IWitnessReportRepository witnessReportRepository;
        

        public UnitOfWork(WitnessReportDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IWitnessReportRepository WitnessReport 
        {
            get
            {
                if(witnessReportRepository == null)
                {
                    this.witnessReportRepository = new WitnessReportRepository(this.dbContext);
                }
                return witnessReportRepository;
            }
        }

        public IDbContextTransaction OpenTransaction()
        {
            return this.dbContext.Database.BeginTransaction();
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
