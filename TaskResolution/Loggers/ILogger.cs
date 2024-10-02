namespace TaskResolution.Loggers
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(Exception ex, string message);
        void LogFatal(string message);
    }
}