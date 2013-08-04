using System;
using System.Collections.Generic;

namespace Tests
{
    public interface ILogger : ILoggerBase, IDisposable
    {
        IEnumerable<ILogAppender> Appenders { get; set; }

        ILogAppender Root { get; set; }

        long Queued { get; }
    }
}