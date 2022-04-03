using ExternalCommunication.Interface;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExternalCommunication.Communication
{
    public class ServiceCommunicator<T> : ICommunicator<T>
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ServiceCommunicator(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetDataAsync(string url)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            
            HttpClient httpClient = this.httpClientFactory.CreateClient();
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string contentString = await httpResponseMessage.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(contentString);
                return result;
            }

            return default(T);
        }
    }
}
