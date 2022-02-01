﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using Matrix.AppSample.HttpApi.Infrastructure.ActionResults;

namespace Matrix.AppSample.HttpApi.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            this._env = env;
            this._logger = loggerFactory.CreateLogger<HttpGlobalExceptionFilter>();
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogCritical(context.Exception, context.Exception.Message);

            var json = new JsonErrorResponse
            {
                Messages = new[] { "An error occur. Try it again." }
            };

            if (_env.IsDevelopment())
                json.DeveloperMessage = context.Exception;

            context.Result = new InternalServerErrorObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }
}
