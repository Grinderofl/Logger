using System;
using System.Collections.Generic;
using NLogger.Appenders;

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

        /// <summary>
        /// Gets or sets debug mode
        /// </summary>
        bool Debug { get; set; }

        /// <summary>
        /// Gets or sets the default logging level
        /// </summary>
        LoggingLevel DefaultLoggingLevel { get; set; }
    }
}