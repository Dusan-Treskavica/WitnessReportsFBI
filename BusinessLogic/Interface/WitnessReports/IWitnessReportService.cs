using Common.Models.WitnessReport;
using System.Collections.Generic;

namespace BusinessLogic.Interface.WitnessReports
{
    public interface IWitnessReportService
    {
        IList<WitnessReport> GetByCaseName(string caseName);
        IList<WitnessReport> GetAll();
        void Save(WitnessReport witnessReport);
    }
}
