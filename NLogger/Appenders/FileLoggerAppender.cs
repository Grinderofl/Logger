using System;
using System.Collections.Generic;

namespace NLogger.Appenders
{
    public class FileLoggerAppender : ILogAppender
    {

        private Queue<LogItem> _queue;

        public LoggingLevel[] LoggingLevels { get; set; }
        public long Queued { get { return _queue.Count; } }
        public string LogPattern { get; set; }
        public string Parameters { get; set; }

        public FileLoggerAppender()
        {
            _queue = new Queue<LogItem>();
        }

        public void Dispose()
        {
            _queue.Clear();
            _queue = null;
        }

        public void Log(string message, Exception exception, LoggingLevel level)
        {
            _queue.Enqueue(new LogItem(message, exception));
        }
        
    }
}
