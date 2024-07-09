using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;

        public WeatherController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string cityName)
        {
            var apiKey = "7a44bda68ab3491b9e412458240907";
            var urlCurrent = $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={cityName}&lang=es";
            var urlForecast = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={cityName}&days=3&lang=es";

            var responseCurrent = await _httpClient.GetStringAsync(urlCurrent);
            var responseForecast = await _httpClient.GetStringAsync(urlForecast);
            var jsonCurrent = JObject.Parse(responseCurrent);
            var jsonForecast = JObject.Parse(responseForecast);

            var weather = new Weather
            {
                City = jsonCurrent["location"]["name"].ToString(),
                Condition = jsonCurrent["current"]["condition"]["text"].ToString(),
                Temperature = (double)jsonCurrent["current"]["temp_c"],
                Humidity = (int)jsonCurrent["current"]["humidity"],
                WindSpeed = (double)jsonCurrent["current"]["wind_kph"],
                Forecast = jsonForecast["forecast"]["forecastday"]
                            .Select(day => new Forecast
                            {
                                Date = day["date"].ToString(),
                                MaxTemp = (double)day["day"]["maxtemp_c"],
                                MinTemp = (double)day["day"]["mintemp_c"],
                                Condition = day["day"]["condition"]["text"].ToString()
                            }).ToList()
            };

            return View("Index", weather);
        }
    }
}
