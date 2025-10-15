namespace Complaint_2._0.Logging
{
    public interface IAppLogger
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, Exception? exception = null, params object[] args);
        void LogDebug(string message, params object[] args);
    }
}
