using LegacyBarber.App.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace LegacyBarber.App.Util.Logging
{
    public class LoggerApp : ILoggerApp
    {
        private readonly ILogger logger;

        public LoggerApp(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger("Application");
        }

        public Task RegisterAsync(string operation, string message, bool success)
        {
            return RegisterInternal(LogLevel.Information, operation, message, success);
        }

        public Task RegisterErrorAsync(string operation, string message, Exception exception)
        {
            logger.LogError(exception, "{Operation} - {Message}", operation, message);
            return Task.CompletedTask;
        }

        public Task RegisterWarningAsync(string operation, string message)
        {
            return RegisterInternal(LogLevel.Warning, operation, message);
        }

        private Task RegisterInternal(LogLevel level, string operation, string message, bool? success = null)
        {
            string status = success.HasValue ? (success.Value ? "Ok" : "Error") : "-";
            logger.Log(level, "{Operation} - {Message} - {Status}", operation, message, status);
            return Task.CompletedTask;
        }
    }
}
