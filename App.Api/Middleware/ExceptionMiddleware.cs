using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LegacyBarber.App.Api.Middleware
{
    /// <summary>
    /// Converts unhandled exceptions into HTTP responses compatible with the
    /// ProblemDetails format (RFC 7807).
    /// </summary>
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            (HttpStatusCode statusCode, string title) = MapException(exception);

            if (statusCode == HttpStatusCode.InternalServerError)
                logger.LogError(exception, "Unhandled exception for request {Method} {Path}", context.Request.Method, context.Request.Path);
            else
                logger.LogWarning(exception, "Handled exception for request {Method} {Path}", context.Request.Method, context.Request.Path);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)statusCode;

            var problem = new ProblemDetailsPayload
            {
                Status = (int)statusCode,
                Title = title,
                Detail = environment.IsDevelopment() ? exception.Message : null,
                Instance = context.Request.Path
            };

            if (exception is ValidationException validationException)
                problem.Extensions["errors"] = validationException.Errors;

            string json = JsonSerializer.Serialize(problem, JsonOptions);
            await context.Response.WriteAsync(json);
        }

        private static (HttpStatusCode, string) MapException(Exception exception)
        {
            return exception switch
            {
                ValidationException => (HttpStatusCode.BadRequest, "Validation failed."),
                ArgumentNullException => (HttpStatusCode.BadRequest, "A required argument was not provided."),
                ArgumentException => (HttpStatusCode.BadRequest, "The request contained an invalid argument."),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Authentication is required."),
                KeyNotFoundException => (HttpStatusCode.NotFound, "The requested resource was not found."),
                InvalidOperationException => (HttpStatusCode.Conflict, "The requested operation is not valid in the current state."),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };
        }

        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        private sealed class ProblemDetailsPayload
        {
            public int Status { get; set; }
            public string? Title { get; set; }
            public string? Detail { get; set; }
            public string? Instance { get; set; }
            public Dictionary<string, object?> Extensions { get; } = new();
        }
    }
}
