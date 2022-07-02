using System;
using System.Collections.Generic;
using System.Net;

namespace Weather.API.Common.Exceptions
{
    public class WeatherMyAppException : Exception
    {
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public WeatherMyAppException()
        {
        }

        public WeatherMyAppException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = message;
        }

        public WeatherMyAppException(string message, HttpStatusCode code) : base(message)
        {
            StatusCode = code;
        }

        public WeatherMyAppException(string message, HttpStatusCode code, IEnumerable<string> errors) : this(message,
            code)
        {
            Errors = errors;
        }
    }
}