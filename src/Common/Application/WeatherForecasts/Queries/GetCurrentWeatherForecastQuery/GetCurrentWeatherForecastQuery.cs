﻿namespace Application.WeatherForecasts.Queries.GetCurrentWeatherForecastQuery
{
    public class GetCurrentWeatherForecastQuery
    {
        public string Q { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int Id { get; set; }
    }
}