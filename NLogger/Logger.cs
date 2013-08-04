using System;
using System.Collections.Generic;

namespace NLogger
{
    public class Logger : ILogger
    {

        #region Fields

        private Queue<LogItem> _queue;

        #endregion


        #region Constructors and destructors

        public Logger()
        {
            _queue = new Queue<LogItem>();
            Appenders = new List<ILogAppender>();
        }

        #endregion
        

        #region Properties

        public IList<LoggingLevel> LoggingLevels { get; set; }

        public IList<ILogAppender> Appenders { get; set; }
        public ILogAppender Root { get; set; }
        public long Queued { get { return _queue.Count; } }
        public bool Debug { get; set; }

        public LoggingLevel DefaultLoggingLevel { get; set; }

        #endregion


        #region ILogger Implemented methods

        public void Log(string message, Exception exception, LoggingLevel level)
        {
            if (LoggingLevels.Contains(level))
                _queue.Enqueue(new LogItem(message, exception));

            for (var i = 0; i < Appenders.Count; i++)
                if ((Appenders[i].LoggingLevels.Contains(level)))
                    Appenders[i].Log(message, exception, level);
            
        }

        #region Log overloads

        public void Log(string message, LoggingLevel level)
        {
            Log(message, null, level);
        }

        public void Log(string message)
        {
            Log(message, DefaultLoggingLevel);
        }

        public void Log(string message, Exception exception)
        {
            Log(message, exception, DefaultLoggingLevel);
        }
        
        public void LogError(string message)
        {
            Log(message, LoggingLevel.Error);
        }

        public void LogError(string message, Exception exception)
        {
            Log(message, exception, LoggingLevel.Error);
        }

        public void LogWarning(string message)
        {
            Log(message, LoggingLevel.Warn);
        }

        public void LogWarning(string message, Exception exception)
        {
            Log(message, exception, LoggingLevel.Warn);
        }

        public void LogInfo(string message)
        {
            Log(message, LoggingLevel.Info);
        }

        public void LogInfo(string message, Exception exception)
        {
            Log(message, exception, LoggingLevel.Info);
        }

        public void LogDebug(string message)
        {
            Log(message, LoggingLevel.Debug);
        }

        public void LogDebug(string message, Exception exception)
        {
            Log(message, exception, LoggingLevel.Debug);
        }

        public void LogTrace(string message)
        {
            Log(message, LoggingLevel.Trace);
        }

        public void LogTrace(string message, Exception exception)
        {
            Log(message, exception, LoggingLevel.Trace);
        }

        #endregion

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