using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ExternalService.Connections.Interfaces
{
    public interface IWeatherClient
    {
        HttpClient Client { get; }
        IConfiguration Configuration { get; }

        public Task<string> GetAllWeatherData(string latitude, string longitude, CancellationToken ctoken);
    }
}
