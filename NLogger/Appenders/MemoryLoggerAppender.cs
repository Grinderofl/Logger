using System;
using System.Collections.Generic;

namespace NLogger.Appenders
{
    public class MemoryLoggerAppender : ILogAppender
    {

        #region Fields

        private Queue<LogItem> _queue;

        #endregion


        #region Properties

        public LoggingLevel[] LoggingLevels { get; set; }
        public long Queued { get { return _queue.Count; } }
        public string LogPattern { get; set; }
        public string Parameters { get; set; }

        public event Logger.LogWritten OnLogWritten;

        #endregion


        #region Constructors and Destructors

        public MemoryLoggerAppender()
        {
            _queue = new Queue<LogItem>();
        }

        #endregion


        public void Dispose()
        {
            _queue.Clear();
            _queue = null;
        }

        public void Log(string message, Exception exception, LoggingLevel level)
        {
            _queue.Enqueue(new LogItem(message, exception, level));
            if (OnLogWritten != null)
                OnLogWritten(new List<LogItem>() {new LogItem(message, exception, level)});
        }
    }
}
