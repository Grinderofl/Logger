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

        public string Message { get; set; }
        public DateTime Created { get; set; }
        public Exception Exception { get; set; }
    }
}
