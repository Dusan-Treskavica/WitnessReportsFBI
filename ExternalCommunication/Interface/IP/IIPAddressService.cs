using Common.Models.IP;
using System.Threading.Tasks;

namespace ExternalCommunication.Interface.IP
{
    public interface IIPAddressService : IBaseCommunicationService
    {
        Task<IPAddress> GetAsync();
    }
}
