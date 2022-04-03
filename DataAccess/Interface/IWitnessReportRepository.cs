using Common.Models.WitnessReport;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace DataAccess.Interface
{
    public interface IWitnessReportRepository
    {
        IList<WitnessReport> GetByCaseName(string caseName);
        IList<WitnessReport> GetAll();
        void Save(WitnessReport witnessReport);

        IDbContextTransaction OpenTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
