using System;
using ExternalService.Connections.Implementations;
using ExternalService.Models;
using Newtonsoft.Json;

namespace ExternalService.Connections.Interfaces
{
    public interface IWeatherData
    {
        FaultDM Fault { get; set; }
        Current Current { get; set; }
        DateTime DateTime { get; set; }

        public static IWeatherData FromJson(string json) => JsonConvert.DeserializeObject<IWeatherData>(json, Converter.Settings);
    }
}
