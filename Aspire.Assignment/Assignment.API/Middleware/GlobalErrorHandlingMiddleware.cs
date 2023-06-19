using Assignment.API.Middleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Assignment.API.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;
        public GlobalErrorHandlingMiddleware(RequestDelegate next,ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory= loggerFactory;
        }
        public async Task Invoke(HttpContext context)
        {
            var _logger = _loggerFactory.CreateLogger<GlobalErrorHandlingMiddleware>();
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var status = HandleStatusCode(ex);
                _logger.LogError($"Something went wrong = Status code :{status}, Exception Message:{ex.Message}, Stack Trace:{ex.StackTrace}");
                throw new Exception(ex.Message);
            }
        }
        private static int HandleStatusCode(Exception exception)
        {
            HttpStatusCode status;
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(BadRequestException))
            {
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                status = HttpStatusCode.NotFound;
            }
            else if (exceptionType == typeof(Exceptions.NotImplementedException))
            {
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(Exceptions.UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(Exceptions.KeyNotFoundException))
            {
                status = HttpStatusCode.Unauthorized;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
            }
            return (int)status;
        }
    }
}
