using System;

namespace NLogger
{
    public interface ILoggerBase
    {
        void Log(string message, LoggingLevel level);
        void Log(string message);
        void Log(string message, Exception exception);
        void Log(string message, Exception exception, LoggingLevel level);

        void LogError(string message);
        void LogError(string message, Exception exception);

        void LogWarning(string message);
        void LogWarning(string message, Exception exception);

        void LogInfo(string message);
        void LogInfo(string message, Exception exception);

        void LogDebug(string message);
        void LogDebug(string message, Exception exception);

        void LogTrace(string message);
        void LogTrace(string message, Exception exception);
    }
}