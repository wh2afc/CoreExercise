using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ExternalService.Connections.Helpers;
using ExternalService.Connections.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ExternalService.Connections.NamedClients
{
    public class OpenWeatherClient : IWeatherClient
    {
        public HttpClient Client { get; }
        public IConfiguration Configuration { get; }

        public OpenWeatherClient(HttpClient client, IConfiguration configuration)
        {
            Client = client;
            Client.DefaultRequestHeaders.ConnectionClose = false;
            Configuration = configuration;
        }

        public async Task<string> GetAllWeatherData(string latitude, string longitude, CancellationToken ctoken)
        {
            string url = Configuration["OpenWeather:WeatherDataEndpoint"]
                .Replace("{lat}", latitude)
                .Replace("{lon}", longitude);

            // Using Mock data to reduce number of hits to the service (or they'll shut me down)
            return MockData.AllWeather;
            
            //return await ApiHelper.MakeAPICall(Client, HttpMethod.Get, url, ctoken);
        }
    }
}
