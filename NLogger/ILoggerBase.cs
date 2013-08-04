using System;

namespace NLogger
{
    public interface ILoggerBase
    {

        /// <summary>
        /// Logs a message with default logging level
        /// </summary>
        /// <param name="message">Message to log</param>
        void Log(string message);
        
        /// <summary>
        /// Logs a message with specified logging level
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="level">Logging level</param>
        void Log(string message, LoggingLevel level);


        /// <summary>
        /// Logs a message with exception and default logging level
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        void Log(string message, Exception exception);

        /// <summary>
        /// Logs a message with exception and specified logging level
        /// </summary>
        /// <param name="message">Message to og</param>
        /// <param name="exception">Exception to log</param>
        /// <param name="level">Logging level</param>
        void Log(string message, Exception exception, LoggingLevel level);


        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogError(string message);

        /// <summary>
        /// Logs an error message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        void LogError(string message, Exception exception);


        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs a warning message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        void LogWarning(string message, Exception exception);


        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs an information message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        void LogInfo(string message, Exception exception);


        /// <summary>
        /// Logs a debug message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogDebug(string message);

        /// <summary>
        /// Logs a debug message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        void LogDebug(string message, Exception exception);

        /// <summary>
        /// Logs a trace message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogTrace(string message);

        /// <summary>
        /// Logs a trace message
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        void LogTrace(string message, Exception exception);
    }
}