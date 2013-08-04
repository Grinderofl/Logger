using System;
using System.Collections.Generic;

namespace Tests
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

        public TimeSpan TimeBetweenLogs { get; set; }
        public int WritesPerMinute { get; set; }
        public LoggingType LoggingType { get; set; }

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