using System;
using AutoMapper;
using Weather.API.Common.Models.ResponseModels;

namespace Weather.API.Common.Mappings
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<Forecast, WeatherForecast>()
                .ForMember(s => s.Date,
                    c => c.MapFrom(q => q.Dt_txt))
                .ForMember(s => s.TemperatureC,
                    c => c.MapFrom(q => (int) q.Main.Temp))
                .ForMember(s => s.TemperatureF, c => c.Ignore())
                .ForMember(s => s.Summary,
                    c => c.MapFrom(q => q.Weather[0]
                        .Description))
                .ForMember(s => s.Clouds,
                    c => c.MapFrom(q => q.Clouds.All))
                .ForMember(s => s.Wind,
                    c => c.MapFrom(q => q.Wind.Speed));

            CreateMap<WeathersClientResponse, WeatherForecastResponse>()
                .ForMember(s => s.WeatherForecasts,
                    q => q.MapFrom(c => c.List))
                .ForMember(s => s.Country,
                    c => c.MapFrom(q => q.City.Country))
                .ForMember(s => s.Name,
                    c => c.MapFrom(q => q.City.Name));

            CreateMap<WeatherClientResponse, ActualWeatherResponse>()
                .ForMember(s => s.TemperatureC,
                    c => c.MapFrom(q => (int) q.Main.Temp))
                .ForMember(s => s.TemperatureF, c => c.Ignore())
                .ForMember(s => s.Summary,
                    c => c.MapFrom(q => q.Weather[0]
                        .Description))
                .ForMember(s => s.Date, c => c.MapFrom(q => DateTime.Now))
                .ForMember(s => s.Clouds,
                    c => c.MapFrom(q => q.Clouds.All))
                .ForMember(s => s.Wind,
                    c => c.MapFrom(q => q.Wind.Speed))
                .ForMember(s => s.Name, c => c.MapFrom(q => q.Name));
        }
    }
}