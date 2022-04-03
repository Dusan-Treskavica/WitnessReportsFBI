using BusinessLogic.Interface.WitnessReports;
using Common.Exceptions;
using Common.Models.CommunicationData;
using Common.Models.IP;
using Common.Models.WitnessReport;
using DataAccess.Interface;
using ExternalCommunication.Interface.FBI;
using ExternalCommunication.Interface.IP;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public class WitnessReportService : IWitnessReportService
    {
        private readonly IWitnessReportRepository witnessReportRepository;
        private readonly IFBICaseService fbiCaseService;
        private readonly IIPAddressService ipAddressService;
        private readonly IWitnessReportValidationService validationService;

        public WitnessReportService(IWitnessReportRepository witnessReportRepository, IFBICaseService fbiCaseService, IIPAddressService ipAddressService, IWitnessReportValidationService witnessReportValidationService)
        {
            this.witnessReportRepository = witnessReportRepository;
            this.fbiCaseService = fbiCaseService;
            this.ipAddressService = ipAddressService;
            this.validationService = witnessReportValidationService;
        }

        public IList<WitnessReport> GetByCaseName(string caseName)
        {
            IList<WitnessReport> witnessReports = this.witnessReportRepository.GetByCaseName(caseName);
            this.validationService.ValidateFoundedWitnessReports(witnessReports);
            return witnessReports;
        }

        public IList<WitnessReport> GetAll()
        {
            return this.witnessReportRepository.GetAll();
        }

        public void Save(WitnessReport witnessReport)
        {
            FBIApiResponse fbiResponse = this.fbiCaseService.GetAsync(witnessReport.CaseName).Result;
            this.validationService.ValidateFBICase(fbiResponse.Items, witnessReport.CaseName);

            IPAddress ipAddress = this.ipAddressService.GetAsync().Result;
            this.validationService.ValidateIPAddress(ipAddress);
            this.validationService.ValidatePhoneNumber(witnessReport.PhoneNumber, ipAddress.Location.Country);

            this.PopulateWitnessReportData(witnessReport, ipAddress);

            try
            {
                this.witnessReportRepository.OpenTransaction();
                this.witnessReportRepository.Save(witnessReport);

                this.witnessReportRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.witnessReportRepository.RollbackTransaction();
                throw new DatabaseException("Not able to store the witness report. Database error.", ex);
            }
        }

        private void PopulateWitnessReportData(WitnessReport witnessReport, IPAddress ipAddress)
        {
            witnessReport.IPAddress = ipAddress.Ip;
            witnessReport.Country = ipAddress.Location.Country;
            witnessReport.Region = ipAddress.Location.Region;
        }
    }
}
