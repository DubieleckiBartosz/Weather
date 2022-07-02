using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Weather.API.Common.Exceptions;
using Weather.API.Wrappers;

namespace Weather.API.Common.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                var model = new ErrorResponse()
                {
                    Message = response.StatusCode > 500 ? "Something went wrong" : ex?.Message,
                    Success = false
                };
                response.ContentType = "application/json";

                switch (ex)
                {
                    case ArgumentNullException e:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        model.Errors ??= new List<string>() {e.InnerException?.Message ?? e.Message};
                        break;
                    case WeatherMyAppException e:
                        response.StatusCode = (int)e.StatusCode;
                        model.Errors = e.Errors?.ToList();
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _logger.LogError("Message: {msg}. Request {method} {url} => {statusCode} - Date: {dateTime}",
                    ex?.Message,
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode,
                    DateTime.Now);

                await response.WriteAsJsonAsync(model);
            }
        }
    }
}
