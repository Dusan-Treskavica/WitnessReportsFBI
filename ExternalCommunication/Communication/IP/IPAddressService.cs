using Common.Models.IP;
using ExternalCommunication.Interface;
using ExternalCommunication.Interface.IP;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ExternalCommunication.Communication.IP
{
    public class IPAddressService : IIPAddressService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICommunicator<IPAddress> serviceCommunicator;
        private readonly IConfiguration configuration;

        public IPAddressService(IHttpContextAccessor httpContextAccessor, ICommunicator<IPAddress> serviceCommunicator, IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.serviceCommunicator = serviceCommunicator;
            this.configuration = configuration;
        }

        public async Task<IPAddress> GetAsync()
        {
            string apiUrl = this.configuration["ipifyApiParams:apiUrl"];
            string apiKey = this.configuration["ipifyApiParams:apiKey"];
            string ip = string.Empty;

            //Since httpContextAccessor retrieves ::1 for IP Address when running application on local machine,
            //here we use DEBUG directive and read testIpAddress from configuration file
#if DEBUG
            ip = this.configuration["ipifyApiParams:testIpAddress"];
#else
            ip = this.httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
#endif

            string url = $"{apiUrl}?apiKey={apiKey}&ipAddress={ip}";

            return await this.serviceCommunicator.GetDataAsync(url);
        }
    }
}
