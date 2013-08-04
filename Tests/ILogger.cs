using System;
using System.Collections.Generic;

namespace Tests
{
    public interface ILogger : IDisposable
    {

        LoggingLevel LogLevels { get; set; }

        TimeSpan TimeBetweenLogs { get; set; }
        int WritesPerMinute { get; set; }

        LoggingType LoggingType { get; set; }

        long Queued { get; }

        void Log(string message, LoggingLevel level);
        void Log(string message);
    }

    public enum LoggingType
    {
        QueueAndThenWrite,
        WritesPerMinute
    }
}