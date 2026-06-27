using LegacyBarber.App.Core.Interfaces;
using LegacyBarber.App.Util.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace LegacyBarber.App.Util
{
    public static class ServiceExtensions
    {
        public static void AddUtilServiceExtensions(this IServiceCollection services)
        {
            services.AddTransient<ILoggerApp, LoggerApp>();
            services.AddLogging();
        }
    }
}
