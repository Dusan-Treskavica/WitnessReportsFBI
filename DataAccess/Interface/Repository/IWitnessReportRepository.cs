using Common.Models.WitnessReport;
using System.Collections.Generic;

namespace DataAccess.Interface.Repository
{
    public interface IWitnessReportRepository
    {
        IList<WitnessReport> GetByCaseName(string caseName);
        IList<WitnessReport> GetAll();
        void Insert(WitnessReport witnessReport);
    }
}
