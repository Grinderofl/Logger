using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger
{
    public class MemoryLoggerAppender : ILogAppender
    {

        #region Fields

        private Queue<string> _queue;

        #endregion


        #region Properties

        public LoggingLevel LogLevels { get; set; }
        public long Queued { get { return _queue.Count; } }
        public string LogPattern { get; set; }
        public string Parameters { get; set; }

        #endregion


        #region Constructors and Destructors

        public MemoryLoggerAppender()
        {
            _queue = new Queue<string>();
        }

        #endregion


        public void Dispose()
        {
            _queue.Clear();
            _queue = null;
        }

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

    }
}
