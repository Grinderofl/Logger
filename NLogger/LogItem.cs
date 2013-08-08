using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger
{
    public class LogItem
    {
        public LogItem(string message, Exception exception = null, LoggingLevel level = LoggingLevel.Info)
        {
            Message = message;
            Exception = exception;
            Created = DateTime.UtcNow;
            Level = level;
            Thread = System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// Message to log
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Logging datetime
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Exception to include with log
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Logging level
        /// </summary>
        public LoggingLevel Level { get; set; }

        /// <summary>
        /// Calling thread
        /// </summary>
        public int Thread { get; set; }

    }
}
