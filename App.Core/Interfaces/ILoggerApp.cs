namespace LegacyBarber.App.Core.Interfaces
{
    public interface ILoggerApp
    {
        Task RegisterAsync(string operation, string message, bool success);
        Task RegisterErrorAsync(string operation, string message, Exception exception);
        Task RegisterWarningAsync(string operation, string message);
    }
}
