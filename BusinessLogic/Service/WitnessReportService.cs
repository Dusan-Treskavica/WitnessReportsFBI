using BusinessLogic.Interface.WitnessReports;
using Common.Exceptions;
using Common.Models.CommunicationData;
using Common.Models.IP;
using Common.Models.WitnessReport;
using DataAccess.Interface.UoW;
using ExternalCommunication.Interface.FBI;
using ExternalCommunication.Interface.IP;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public class WitnessReportService : IWitnessReportService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFBICaseService fbiCaseService;
        private readonly IIPAddressService ipAddressService;
        private readonly IWitnessReportValidationService validationService;

        public WitnessReportService(IUnitOfWork unitOfWork, IFBICaseService fbiCaseService, IIPAddressService ipAddressService, IWitnessReportValidationService witnessReportValidationService)
        {
            this.unitOfWork = unitOfWork;
            this.fbiCaseService = fbiCaseService;
            this.ipAddressService = ipAddressService;
            this.validationService = witnessReportValidationService;
        }

        public IList<WitnessReport> GetByCaseName(string caseName)
        {
            IList<WitnessReport> witnessReports = this.unitOfWork.WitnessReport.GetByCaseName(caseName);
            this.validationService.ValidateFoundedWitnessReports(witnessReports);
            return witnessReports;
        }

        public IList<WitnessReport> GetAll()
        {
            return this.unitOfWork.WitnessReport.GetAll();
        }

        public void Save(WitnessReport witnessReport)
        {
            FBIApiResponse fbiResponse = this.fbiCaseService.GetAsync(witnessReport.CaseName).Result;
            this.validationService.ValidateFBICase(fbiResponse.Items, witnessReport.CaseName);

            IPAddress ipAddress = this.ipAddressService.GetAsync().Result;
            this.validationService.ValidateIPAddress(ipAddress);
            this.validationService.ValidatePhoneNumber(witnessReport.PhoneNumber, ipAddress.Location.Country);

            this.PopulateWitnessReportData(witnessReport, ipAddress);

            using (IDbContextTransaction transaction = this.unitOfWork.OpenTransaction())
            {
                try
                {
                    this.unitOfWork.WitnessReport.Insert(witnessReport);
                    this.unitOfWork.Save();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new DatabaseException("Not able to store the witness report. Database error.", ex);
                }
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
