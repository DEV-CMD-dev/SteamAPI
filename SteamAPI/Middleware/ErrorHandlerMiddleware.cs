using Microsoft.AspNetCore.Mvc;
using System.Net;
using BusinessLogic;

namespace SteamApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpException ex)
            {
                _logger.LogWarning(ex, ex.Message);

                await SendResponse(context, ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await SendResponse(
                    context,
                    "An unexpected error occurred.",
                    HttpStatusCode.InternalServerError);
            }
        }

        private static async Task SendResponse(
            HttpContext context,
            string message,
            HttpStatusCode statusCode)
        {
            if (context.Response.HasStarted)
                return;

            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "Error",
                Detail = message,
                Status = (int)statusCode
            });
        }
    }

    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandler(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}