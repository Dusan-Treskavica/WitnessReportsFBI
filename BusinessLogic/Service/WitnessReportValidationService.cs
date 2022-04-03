using BusinessLogic.Interface.WitnessReports;
using Common.Constants;
using Common.Exceptions;
using Common.Models.FBI;
using Common.Models.IP;
using Common.Models.WitnessReport;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Service
{
    public class WitnessReportValidationService : IWitnessReportValidationService
    {
        private const string INVALID_COUNTRY = "ZZ";

        public void ValidateFBICase(IList<FBICase> fbiCases, string caseName)
        {
            if (fbiCases == null || fbiCases.Count == 0 || !fbiCases.Any(x => x.Title.Equals(caseName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new RequestProcessingException("FBI Case with provided Name does not exist. ", HTTPResponseCodes.BAD_REQUEST);
            }
        }

        public void ValidateIPAddress(IPAddress ipAddress)
        {
            if (ipAddress == null || ipAddress.Location.Country == INVALID_COUNTRY)
            {
                throw new RequestProcessingException("Not valid IP address. IP address not found. ", HTTPResponseCodes.BAD_REQUEST);
            }
        }

        public void ValidatePhoneNumber(string phoneNumber, string countryCode)
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber phone = phoneNumberUtil.Parse(phoneNumber, countryCode);

            if (!phoneNumberUtil.IsValidNumber(phone))
            {
                throw new RequestProcessingException(@"Invalid phone number.", HTTPResponseCodes.BAD_REQUEST);
            }
            if (!phoneNumberUtil.IsValidNumberForRegion(phone, countryCode))
            {
                throw new RequestProcessingException("Provided phone number doesn't belong to the country from which the request was sent.", HTTPResponseCodes.BAD_REQUEST);
            }
        }

        public void ValidateFoundedWitnessReports(IList<WitnessReport> witnessReports)
        {
            if (witnessReports == null || witnessReports.Count == 0)
            {
                throw new RequestProcessingException("There is no founded witness reports with specified case name.", HTTPResponseCodes.NOT_FOUND);
            }
        }
    }
}
