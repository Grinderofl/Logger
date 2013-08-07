using System;

namespace NLogger
{
    public static class Log
    {
        private static readonly ILogger Instance;

        static Log()
        {
            Instance = new Logger().Initialize();
        }

        private static void LogMessage(string message, Exception exception = null,
                                       LoggingLevel level = LoggingLevel.Info)
        {
            Instance.Log(message, exception, level);
        }

        /// <summary>
        ///     Logs a fatal message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Fatal(string message)
        {
            LogMessage(message, null, LoggingLevel.Fatal);
        }

        /// <summary>
        ///     Logs a fatal message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        public static void Fatal(string message, Exception exception)
        {
            LogMessage(message, exception, LoggingLevel.Fatal);
        }


        /// <summary>
        ///     Logs an error message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Error(string message)
        {
            LogMessage(message, null, LoggingLevel.Error);
        }

        /// <summary>
        ///     Logs an error message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        public static void Error(string message, Exception exception)
        {
            LogMessage(message, exception, LoggingLevel.Error);
        }


        /// <summary>
        ///     Logs a warning message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Warning(string message)
        {
            LogMessage(message, null, LoggingLevel.Warning);
        }

        /// <summary>
        ///     Logs a warning message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        public static void Warning(string message, Exception exception)
        {
            LogMessage(message, exception, LoggingLevel.Warning);
        }


        /// <summary>
        ///     Logs an information message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Info(string message)
        {
            LogMessage(message);
        }

        /// <summary>
        ///     Logs an information message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        public static void Info(string message, Exception exception)
        {
            LogMessage(message, exception);
        }


        /// <summary>
        ///     Logs a debug message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Debug(string message)
        {
            LogMessage(message, null, LoggingLevel.Debug);
        }

        /// <summary>
        ///     Logs a debug message with exception
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        public static void Debug(string message, Exception exception)
        {
            LogMessage(message, exception, LoggingLevel.Debug);
        }

        /// <summary>
        ///     Logs a trace message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Trace(string message)
        {
            LogMessage(message, null, LoggingLevel.Trace);
        }

        /// <summary>
        ///     Logs a trace message
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception to log</param>
        public static void Trace(string message, Exception exception)
        {
            LogMessage(message, exception, LoggingLevel.Trace);
        }


        public static void FatalFormat(string message, params object[] args)
        {
            Fatal(string.Format(message, args));
        }

        public static void ErrorFormat(string message, params object[] args)
        {
            Error(string.Format(message, args));
        }

        public static void WarningFormat(string message, params object[] args)
        {
            Warning(string.Format(message, args));
        }

        public static void DebugFormat(string message, params object[] args)
        {
            Debug(string.Format(message, args));
        }

        public static void InfoFormat(string message, params object[] args)
        {
            Info(string.Format(message, args));
        }

        public static void TraceFormat(string message, params object[] args)
        {
            Trace(string.Format(message, args));
        }
    }
}