using System;
using System.Collections.Generic;
using System.Configuration;
using NLogger.Appenders;
using NLogger.Configuration;

namespace NLogger
{
    public class Logger : ILogger
    {

        public static string FormatLog(string format, LogItem item)
        {
            return
                format.Replace("%date", item.Created.ToString("yyyy/MM/dd HH:mm:ss.fffffff"))
                      .Replace("%shortdate", item.Created.ToString("yyyy/MM/dd HH:mm:ss"))
                      .Replace("%message", item.Message)
                      .Replace("%level", item.Level.ToString());
        }

        public delegate void LogWritten(IList<LogItem> logItems);

        #region Constructors and destructors

        public Logger()
        {
            Root = new RootAppender();
            Appenders = new List<ILogAppender>();
        }

        public ILogger Initialize(NLoggerConfigurationSection config = null)
        {
            if(config == null)
                config = ConfigurationManager.GetSection("NLoggerConfiguration") as NLoggerConfigurationSection;

            if(config == null) throw new ConfigurationErrorsException("No configuration section found");
            if (config.Root != null)
                Root.LoggingLevels = GetLoggingLevels(config.Root);


            foreach (NLoggerAppender item in config.Appenders)
            {
                ILogAppender appender;
                if (item.Type.ToLower().Contains("fileloggerappender"))
                    appender = new FileLoggerAppender();
                else if (item.Type.ToLower().Contains("consoleloggerappender"))
                    appender = new ConsoleLoggerAppender();
                else
                    appender = new MemoryLoggerAppender();

                appender.Parameters = item.Parameters;
                appender.LoggingLevels = GetLoggingLevels(item);
                if (item.Pattern != null)
                    appender.LogPattern = item.Pattern.Value;

                Appenders.Add(appender);
            }
            return this;
        }

        private static LoggingLevel[] GetLoggingLevels(Configuration.RootAppender appender)
        {
            if(appender == null)
                return new LoggingLevel[0];
            var list = new List<LoggingLevel>();
            if (appender.Level.Fatal)
                list.Add(LoggingLevel.Fatal);
            if (appender.Level.Error)
                list.Add(LoggingLevel.Error);
            if (appender.Level.Warning)
                list.Add(LoggingLevel.Warning);
            if (appender.Level.Debug)
                list.Add(LoggingLevel.Debug);
            if (appender.Level.Info)
                list.Add(LoggingLevel.Info);
            if (appender.Level.Trace)
                list.Add(LoggingLevel.Trace);

            return list.ToArray();
        }

        #endregion
        

        #region Properties

        public IList<ILogAppender> Appenders { get; set; }
        public ILogAppender Root { get; set; }
        public long Queued { get { return Root.Queued; } }
        public bool Debug { get; set; }

        public LoggingLevel DefaultLoggingLevel { get; set; }

        #endregion


        #region ILogger Implemented methods

        public void Log(string message, Exception exception, LoggingLevel level)
        {
            bool rootlevel = Root.LoggingLevels.Contains(level);

            if (rootlevel)
                Root.Log(message, exception, level);

            for (var i = 0; i < Appenders.Count; i++)
            {
                if (Appenders[i].LoggingLevels.Length == 0)
                {
                    if(rootlevel)
                        Appenders[i].Log(message, exception, level);
                    continue;
                }

                if ((Appenders[i].LoggingLevels.Contains(level)))
                    Appenders[i].Log(message, exception, level);
            }

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

        public void LogFatal(string message)
        {
            Log(message, LoggingLevel.Fatal);
        }

        public void LogFatal(string message, Exception exception)
        {
            Log(message, exception, LoggingLevel.Fatal);
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
            Log(message, LoggingLevel.Warning);
        }

        public void LogWarning(string message, Exception exception)
        {
            Log(message, exception, LoggingLevel.Warning);
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
            Root.Dispose();
            for(var i = 0; i < Appenders.Count; i++)
                Appenders[i].Dispose();

            Appenders = null;
        }

        #endregion


        #region Private methods

        private LoggingLevel GetLoggingLevel(string value)
        {
            return (LoggingLevel) Enum.Parse(typeof (LoggingLevel), value);
        }

        #endregion


    }
}