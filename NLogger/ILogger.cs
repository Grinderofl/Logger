using System;
using System.Collections.Generic;

namespace NLogger
{
    public interface ILogger : ILoggerBase, IDisposable
    {
        /// <summary>
        /// Logger appenders
        /// </summary>
        IList<ILogAppender> Appenders { get; set; }

        /// <summary>
        /// Logger root
        /// </summary>
        ILogAppender Root { get; set; }

        /// <summary>
        /// Number of items in the queue
        /// </summary>
        long Queued { get; }

        bool Debug { get; set; }
    }
}