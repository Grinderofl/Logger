using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLogger.Appenders;

namespace NLogger
{
    internal class RootAppender : ILogAppender
    {

        #region Fields

        private Queue<LogItem> _queue;

        #endregion


        #region Constructors and Destructors

        public RootAppender()
        {
            _queue = new Queue<LogItem>();
        }

        #endregion


        public void Log(string message)
        {
            throw new NotImplementedException();
        }

        public void Log(string message, LoggingLevel level)
        {
            throw new NotImplementedException();
        }

        public void Log(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Log(string message, Exception exception, LoggingLevel level)
        {
            _queue.Enqueue(new LogItem(message, exception));
        }

        public void LogError(string message)
        {
            throw new NotImplementedException();
        }

        public void LogError(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string message)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(string message)
        {
            throw new NotImplementedException();
        }

        public void LogDebug(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(string message)
        {
            throw new NotImplementedException();
        }

        public void LogTrace(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _queue.Clear();
            _queue = null;
        }

        public LoggingLevel[] LoggingLevels { get; set; }
        public long Queued { get { return _queue.Count; } }
        public string LogPattern { get; set; }
        public string Parameters { get; set; }
    }
}
