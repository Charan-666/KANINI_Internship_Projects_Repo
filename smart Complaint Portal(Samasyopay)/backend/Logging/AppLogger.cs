namespace Complaint_2._0.Logging
{
    public class AppLogger : IAppLogger
    {
        private readonly ILogger<AppLogger> _logger;

        public AppLogger(ILogger<AppLogger> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogError(string message, Exception? exception = null, params object[] args)
        {
            if (exception != null)
                _logger.LogError(exception, message, args);
            else
                _logger.LogError(message, args);
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }
    }
}