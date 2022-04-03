using Common.Models.CommunicationData;
using System.Threading.Tasks;

namespace ExternalCommunication.Interface.FBI
{
    public interface IFBICaseService : IBaseCommunicationService
    {
        Task<FBIApiResponse> GetAsync(string title);
    }
}
