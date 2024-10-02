using log4net;

namespace TaskResolution.Loggers
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog log;

        public Log4NetLogger(Type type)
        {
            log = LogManager.GetLogger(type);
        }

        public void LogDebug(string message)
        {
            log.Debug(message);
        }

        public void LogInfo(string message)
        {
            log.Info(message);
        }

        public void LogWarning(string message)
        {
            log.Warn(message);
        }

        public void LogError(Exception ex, string message)
        {
            log.Error(message, ex);
        }

        public void LogFatal(string message)
        {
            log.Fatal(message);
        }
    }
}