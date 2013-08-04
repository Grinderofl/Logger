using System;

namespace NLogger
{
    public interface ILogAppender : IDisposable, ILoggerBase
    {
        LoggingLevel LogLevels { get; set; }

        long Queued { get; }
    }
}