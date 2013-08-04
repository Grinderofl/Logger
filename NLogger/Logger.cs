using System;
using System.Collections.Generic;

namespace NLogger
{
    public class Logger : ILogger
    {

        #region Constructors and destructors

        public Logger()
        {
            _queue = new Queue<string>();
        }

        #endregion


        #region Fields

        private Queue<string> _queue;

        #endregion


        #region Properties

        public LoggingLevel LogLevels { get; set; }

        public IEnumerable<ILogAppender> Appenders { get; set; }
        public ILogAppender Root { get; set; }
        public long Queued { get { return _queue.Count; } }

        #endregion


        #region ILogger Implemented methods

        public void Log(string message, LoggingLevel level)
        {
            if((LogLevels & level) == level)
                _queue.Enqueue(message);
        }

        public void Log(string message)
        {
            Log(message, LoggingLevel.Info);
        }

        public void Log(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Log(string message, Exception exception, LoggingLevel level)
        {
            throw new NotImplementedException();
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

        #endregion


        #region IDisposable Implemented Methods

        public void Dispose()
        {
            _queue.Clear();
            _queue = null;
        }

        #endregion


        #region Private methods

        #endregion


    }
}