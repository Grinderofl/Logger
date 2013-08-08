using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger.Appenders
{
    /// <summary>
    /// LogAppender abstract interface which implements fluent configuration
    /// </summary>
    public abstract class LogAppender : ILogAppenderFluent
    {
        protected LogAppender()
        {
            LoggingLevels = new LoggingLevel[0];
        }

        public virtual void Log(string message, Exception exception, LoggingLevel level)
        {
        }

        public virtual void Dispose()
        {
            
        }
        

        public string Name { get; set; }
        public LoggingLevel[] LoggingLevels { get; set; }
        public long Queued { get; private set; }
        public string LogPattern { get; set; }
        public string Parameters { get; set; }
        public TimeSpan TimeSinceLastWrite { get; set; }
        public int MaxQueueCache { get; set; }
        public int TimeBetweenChecks { get; set; }
        public string MaxFileSize { get; set; }
        public string Location { get; set; }
        public int MaxLogCount { get; set; }
        public event Logger.LogWritten OnLogWritten;

        public ILogAppenderFluent AddLevel(LoggingLevel level)
        {
            var temp = LoggingLevels;
            if (temp == null)
                LoggingLevels = new LoggingLevel[] {level};
            else
            {
                LoggingLevels = new LoggingLevel[temp.Count() + 1];
                temp.CopyTo(LoggingLevels, 0);
                LoggingLevels[temp.Count()] = level;
            }
            
            return this;
        }

        protected LogAppender(string name, string pattern = "", string parameters = "",
                                                     TimeSpan timeSinceLastWrite = new TimeSpan(), int maxQueueCache = 100,
                                                     int timeBetweenChecks = 50, string maxFileSize = "10MB", string location = "",
                                                     int maxLogCount = 0)
        {
            Name = name;
            LogPattern = pattern;
            Parameters = parameters;
            TimeSinceLastWrite = timeSinceLastWrite;
            MaxQueueCache = maxQueueCache;
            TimeBetweenChecks = timeBetweenChecks;
            MaxFileSize = maxFileSize;
            Location = location;
            MaxLogCount = maxLogCount;
        }

        ILogAppenderFluent ILogAppenderFluent.OnLogWritten(Logger.LogWritten logWritten)
        {
            OnLogWritten += logWritten;
            return this;
        }

        protected void DefaultLogWriter(IList<LogItem> logItems)
        {
            OnLogWritten(logItems);
        }
    }
}
