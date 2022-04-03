using System.Threading.Tasks;

namespace ExternalCommunication.Interface
{
    public interface ICommunicator<T>
    {
        Task<T> GetDataAsync(string url); 
    }
}
