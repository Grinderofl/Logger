using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger
{
    public class LogItem
    {
        public LogItem(string message, Exception exception = null)
        {
            Message = message;
            Exception = exception;
            Created = DateTime.UtcNow;
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
    }
}
