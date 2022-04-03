using Common.Models.CommunicationData;
using ExternalCommunication.Interface;
using ExternalCommunication.Interface.FBI;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ExternalCommunication.Communication.FBI
{
    public class FBICaseService : IFBICaseService
    {
        private readonly ICommunicator<FBIApiResponse> serviceCommunicator;
        private readonly IConfiguration configuration;

        public FBICaseService(ICommunicator<FBIApiResponse> serviceCommunicator, IConfiguration configuration)
        {
            this.serviceCommunicator = serviceCommunicator;
            this.configuration = configuration;
        }

        public async Task<FBIApiResponse> GetAsync(string title)
        {
            string apiUrl = this.configuration["fbiApiParams:apiUrl"];
            string url = $"{apiUrl}?title={title}";
            
            return await this.serviceCommunicator.GetDataAsync(url);
        }
    }
}
