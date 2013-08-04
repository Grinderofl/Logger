using System;

namespace Tests
{
    public interface ILogAppender : IDisposable, ILoggerBase
    {
        LoggingLevel LogLevels { get; set; }

        long Queued { get; }
    }
}