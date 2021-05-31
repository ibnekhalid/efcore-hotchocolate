using HotChocolate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace Tm.Api.Extensions.GraphQL
{
    public class GraphQlErrorFilter : IErrorFilter
    {
        private readonly ILogger<GraphQlErrorFilter> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string ServerErrorMessage = "Internal Server Error";
        private const int ServerErrorCode = 500;

        public GraphQlErrorFilter(ILogger<GraphQlErrorFilter> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IError OnError(IError error)
            => HandleException(error);

        private IError HandleException(IError error)
        {

            var buildError = error.Exception switch
            {
                Exception exception => HandleException(error, exception),
                _ => HandleException(error, error.Exception)
            };
            if (!int.TryParse(buildError.Code, out int code) || code <= 499) return buildError;

            error.SetExtension("TraceId", _httpContextAccessor?.HttpContext?.TraceIdentifier);
            error.SetExtension("RequestId", _httpContextAccessor?.HttpContext?.Request.Headers["CorrelationId"].ToString());
            _logger.LogError(error.Exception, "{message}. Code: {code}. Path: {path}. GraphQLCode: {GraphQLCode}. ", error.Message, code, error.Path, error.Code);
            return buildError;
        }

        private static IError HandleException(IError error, Exception exception)
        {
            if (exception is null)
                return error;
            return new ErrorBuilder()
                  .SetMessage(exception.Message)
                  .SetExtension("type", exception.Message)
                  .Build();
        }

   
    }
}
