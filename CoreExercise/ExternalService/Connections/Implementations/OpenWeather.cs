// JSON generated using QuickType;
//
//    var weatherModel = IWeatherData.FromJson(jsonString);

using System;
using System.Collections.Generic;
using System.Globalization;
using ExternalService.Connections.Interfaces;
using ExternalService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ExternalService.Connections.Implementations
{
    public class OpenWeather : IWeatherData
    {

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }

        [JsonProperty("hourly")]
        public List<Hourly> Hourly { get; set; }

        [JsonProperty("daily")]
        public List<Daily> Daily { get; set; }

        [JsonProperty("fault")]
        public FaultDM Fault { get; set; }

        [JsonIgnore]
        public DateTime DateTime { get; set; }

    }

    public partial class Current
    {
        [JsonProperty("dt")]
        public long Timestamp { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("uvi")]
        public double UVIndex { get; set; }

        [JsonProperty("clouds")]
        public long Clouds { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")]
        public long WindDeg { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("rain")]
        public Rain Rain { get; set; }
    }

    public partial class Rain
    {
        [JsonProperty("1h")]
        public double The1H { get; set; }
    }

    public partial class Weather
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public partial class Daily
    {
        [JsonProperty("dt")]
        public long Timestamp { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }

        [JsonProperty("temp")]
        public Temp Temp { get; set; }

        [JsonProperty("feels_like")]
        public FeelsLike FeelsLike { get; set; }

        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")]
        public long WindDeg { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("clouds")]
        public long Clouds { get; set; }

        [JsonProperty("rain")]
        public double Rain { get; set; }

        [JsonProperty("uvi")]
        public double UVIndex { get; set; }
    }

    public partial class FeelsLike
    {
        [JsonProperty("day")]
        public double Day { get; set; }

        [JsonProperty("night")]
        public double Night { get; set; }

        [JsonProperty("eve")]
        public double Evening { get; set; }

        [JsonProperty("morn")]
        public double Morning { get; set; }
    }

    public partial class Temp
    {
        [JsonProperty("day")]
        public double Day { get; set; }

        [JsonProperty("min")]
        public double Min { get; set; }

        [JsonProperty("max")]
        public double Max { get; set; }

        [JsonProperty("night")]
        public double Night { get; set; }

        [JsonProperty("eve")]
        public double Evening { get; set; }

        [JsonProperty("morn")]
        public double Morning { get; set; }
    }

    public partial class Hourly
    {
        [JsonProperty("dt")]
        public long Timestamp { get; set; }

        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("clouds")]
        public long Clouds { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")]
        public long WindDeg { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }

        [JsonProperty("rain")]
        public Rain Rain { get; set; }
    }

    //public partial class OpenWeather
    //{
    //    public static OpenWeather FromJson(string json) => JsonConvert.DeserializeObject<OpenWeather>(json, Converter.Settings);
    //}

    public static class Serialize
    {
        public static string ToJson(this OpenWeather self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

    }
}

/*
Please, use your API key in each API call.

We do not process API requests without the API key.

API call:
http://api.openweathermap.org/data/2.5/forecast?id=524901&APPID={APIKEY}
Parameters:
APPID {APIKEY} is your unique API key
Example of API call:
api.openweathermap.org/data/2.5/forecast?id=524901&APPID=1111111111
Tips on how to use API effectively
*/

/*
Tips on how to use API effectively
 We recommend making calls to the API no more than one time every 10 minutes for one location (city / coordinates / zip-code). 
 This is due to the fact that weather data in our system is updated no more than one time every 10 minutes.

 The server name is api.openweathermap.org. Please, never use the IP address of the server.

 Better call API by city ID instead of a city name, city coordinates or zip code to get a precise result. The list of cities' IDs is here.

 Please, mind that Free and Startup accounts have limited service availability. If you do not receive a response from the API, 
 please, wait at least for 10 min and then repeat your request. We also recommend you to store your previous request.
*/
