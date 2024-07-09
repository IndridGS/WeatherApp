namespace WeatherApp.Models
{
    public class Weather
    {
        public string City { get; set; }
        public string Condition { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public List<Forecast> Forecast { get; set; }
    }

    public class Forecast
    {
        public string Date { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string Condition { get; set; }
    }

}
