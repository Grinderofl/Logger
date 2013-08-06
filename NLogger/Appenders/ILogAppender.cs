using System;
using System.Collections.Generic;

namespace NLogger.Appenders
{
    public interface ILogAppender : ILoggerBase, IDisposable
    {
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

        string MaxFileSize { get; set; }

        string Location { get; set; }

        int MaxLogCount { get; set; }

        event Logger.LogWritten OnLogWritten;
    }
}