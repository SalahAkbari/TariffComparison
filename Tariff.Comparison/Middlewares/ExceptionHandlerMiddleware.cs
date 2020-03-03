﻿using Tariff.Comparison.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Tariff.Framework.Exceptions;
using System.Collections.Generic;

namespace Tariff.Comparison.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var message = CreateMessage(context, ex);
                _logger.LogError(message, ex);

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var result = new List<TariffResultModel>() { new TariffResultModel { IsSuccessful = false, Message = e.Message } };
            int statusCode;

           if (e is TariffException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
                result[0].Message = "Unknown error, please contact the system admin";
            }

            _logger.LogError(e, e.Message);

            var response = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }

        private string CreateMessage(HttpContext context, Exception e)
        {
            var message = $"Exception caught in global error handler, exception message: {e.Message}, exception stack: {e.StackTrace}";

            if (e.InnerException != null)
            {
                message = $"{message}, inner exception message {e.InnerException.Message}, inner exception stack {e.InnerException.StackTrace}";
            }

            return $"{message} RequestId: {context.TraceIdentifier}";
        }
    }    
}
