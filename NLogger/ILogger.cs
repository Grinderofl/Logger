using System;
using System.Collections.Generic;

namespace NLogger
{
    public interface ILogger : ILoggerBase, IDisposable
    {
        IEnumerable<ILogAppender> Appenders { get; set; }

        ILogAppender Root { get; set; }

        long Queued { get; }
    }
}