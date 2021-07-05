using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shop.Business
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
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
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = new
                {
                    message = ex.Message,
                    statusCode = response.StatusCode
                };

                Log.Error($"{errorResponse}");
                var errorJson = JsonSerializer.Serialize(errorResponse);

                await response.WriteAsync(errorJson);
            }
        }

        public static void AppUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (Log.Logger != null && e.ExceptionObject is Exception exception)
            {
                UnhandledExceptions(exception);

                if (e.IsTerminating)
                {
                    Log.CloseAndFlush();
                }
            }
        }

        public static void UnhandledExceptions(Exception e)
        {
            Log.Logger?.Error(e, "Application crashed");
        }
    }
}
