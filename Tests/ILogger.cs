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
}