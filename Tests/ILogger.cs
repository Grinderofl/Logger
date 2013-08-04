using System;
using System.Collections.Generic;

namespace Tests
{
    public interface ILogger : IDisposable
    {

        LoggingLevel LogLevels { get; set; } 

        long Queued { get; }

        void Log(string message, LoggingLevel level);
    }

    public enum LoggingLevel : byte
    {
        Error = 1 << 1,
        Warn = 1 << 2,
        Info = 1 << 3,
        Debug = 1 << 4,
        Trace = 1 << 5
    }
}