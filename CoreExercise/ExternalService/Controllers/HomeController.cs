using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ExternalService.Connections.Implementations;
using ExternalService.Connections.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExternalService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ExternalService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;
        private IWeatherClient _weatherClient;
        private IWeatherData _weatherData;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
            IWeatherClient weatherClient, IWeatherData weatherData)
        {
            _logger = logger;
            _configuration = configuration;
            _weatherClient = weatherClient;
            _weatherData = weatherData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(CancellationToken ctoken)
        {
            IWeatherData model;
            string latitude = _configuration["Location:SLC:Lat"];
            string longitude = _configuration["Location:SLC:Lon"];
            if (_weatherData is OpenWeather data)
            {
                model = data;
                string results = string.Empty;
                try
                {
                    results = await _weatherClient.GetAllWeatherData(latitude, longitude, ctoken);
                }
                catch (Exception ex)
                {
                    return new JsonResult($"Error getting weather data: {results}, {ex}");
                }

                try
                {
                    model =  JsonConvert.DeserializeObject<OpenWeather>(results);
                }
                catch (Exception ex)
                {
                    model.Fault = JsonConvert.DeserializeObject<FaultDM>(results);
                }

                try
                {
                    //model.Fault = new FaultDM(){Code=500, Message="Testing out fault response"};
                    if (model.Current != null && model.Current.Timestamp > 0)
                    {
                        model.DateTime = new DateTime(1970,1,1).AddSeconds(model.Current.Timestamp).ToLocalTime();
                    }
                }
                catch (Exception ex)
                {
                    model.Fault = new FaultDM(){Code=500, Message=ex.ToString()};
                }
                return PartialView(model);
            }

            return new JsonResult("{\"cod\":\"500\", \"message\":\"Invalid weather data: A known weather provider was not injected.\"}");
        }
    }
}
