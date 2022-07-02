using System;
using System.Linq;
using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using Weather.API.Common.Interfaces;
using Weather.API.Common.Mappings;
using Weather.API.Enums;
using Weather.API.Helpers;

namespace Application.UnitTests.Services.WeatherService
{
    public abstract class WeatherServiceBaseTests
    {
        protected readonly Mock<IWeatherClientService> _weatherClientServiceMock;
        protected readonly Mock<ICacheService> _cacheServiceMock;
        protected readonly Mock<ILogger<Weather.API.Services.WeatherService>> _logger;
        protected IMapper _mapper;
        protected readonly Fixture _fixture;
        private readonly AutoMocker _mocker;
        protected readonly Weather.API.Services.WeatherService _service;

        protected WeatherServiceBaseTests()
        {
            this._fixture = new Fixture();
            this._mocker = new AutoMocker();
            this._mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WeatherProfile>();
            }).CreateMapper();

            this._weatherClientServiceMock = this._mocker.GetMock<IWeatherClientService>();
            this._cacheServiceMock = this._mocker.GetMock<ICacheService>();
            this._logger = this._mocker.GetMock<ILogger<Weather.API.Services.WeatherService>>();
            this._mocker.Use(this._mapper);
            this._service = this._mocker.CreateInstance<Weather.API.Services.WeatherService>();
        }

        protected string GetCountry()
        {
            var enumType = EnumHelpers.GetEnumValues<CountryCodes>().ToList();
            var _rnd = new Random().Next(0, enumType.Count - 1);
            return enumType[_rnd];
        }
    }
}
