using Common.Models.FBI;
using Common.Models.IP;
using Common.Models.WitnessReport;
using System.Collections.Generic;

namespace BusinessLogic.Interface.WitnessReports
{
    public interface IWitnessReportValidationService
    {
        void ValidateFBICase(IList<FBICase> fbiCase, string caseName);
        void ValidateIPAddress(IPAddress ipAddress);
        void ValidatePhoneNumber(string phoneNumber, string countryCode);

        void ValidateFoundedWitnessReports(IList<WitnessReport> witnessReports);
    }
}
