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

        public void Log(string message, LoggingLevel level)
        {
            throw new NotImplementedException();
        }

        public void Log(string message)
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

        
    }
}
