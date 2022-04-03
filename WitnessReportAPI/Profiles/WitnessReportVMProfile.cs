using AutoMapper;
using Common.Models.WitnessReport;
using WitnessReportAPI.ViewModel;

namespace WitnessReportAPI.Profiles
{
    public class WitnessReportVMProfile : Profile
    {
        public WitnessReportVMProfile()
        {
            CreateMap<WitnessReportVMRequest, WitnessReport>();
            
            CreateMap<WitnessReport, WitnessReportVMResponse>();
            
        }
    }
}
