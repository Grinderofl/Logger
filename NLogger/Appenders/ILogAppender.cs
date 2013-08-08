using System;
using System.Collections.Generic;

namespace NLogger.Appenders
{
    /// <summary>
    /// LogAppender interface
    /// </summary>
    public interface ILogAppender : ILoggerBase, IDisposable
    {
        /// <summary>
        /// Appender name
        /// </summary>
        string Name { get; set; }

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

        /// <summary>
        /// Maximum amount of time to cache data between writes
        /// </summary>
        TimeSpan TimeSinceLastWrite { get; set; }

        /// <summary>
        /// Maximum number of items in queue before writes
        /// </summary>
        int MaxQueueCache { get; set; }

        /// <summary>
        /// Time between cache write checks in milliseconds
        /// </summary>
        int TimeBetweenChecks { get; set; }

        /// <summary>
        /// Maximum allowed log file size / database size / whatever size
        /// </summary>
        string MaxFileSize { get; set; }

        /// <summary>
        /// Location for log file
        /// </summary>
        string Location { get; set; }

        /// <summary>
        /// Maximum number of logs kept as backup
        /// </summary>
        int MaxLogCount { get; set; }

        /// <summary>
        /// Log is being written
        /// </summary>
        event Logger.LogWritten OnLogWritten;
    }
}