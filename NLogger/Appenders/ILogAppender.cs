using System;

namespace NLogger.Appenders
{
    public interface ILogAppender : ILoggerBase, IDisposable
    {
        /// <summary>
        /// Logging level
        /// </summary>
        LoggingLevel[] LoggingLevels { get; set; }

        /// <summary>
        /// Number of items in the log queue
        /// </summary>
        long Queued { get; }

        /// <summary>
        /// Pattern for logging
        /// </summary>
        string LogPattern { get; set; }

        /// <summary>
        /// Parameters for logging
        /// </summary>
        string Parameters { get; set; }
    }
}