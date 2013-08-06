﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger.Appenders
{
    public class ConsoleLoggerAppender : ILogAppender
    {
        public void Log(string message, Exception exception, LoggingLevel level)
        {
            Console.WriteLine(Logger.FormatLog(string.IsNullOrEmpty(LogPattern) ? DefaultLogPattern : LogPattern, new LogItem(message, exception, level)));
            if (OnLogWritten != null)
                OnLogWritten(new List<LogItem>() {new LogItem(message, exception, level)});
        }

        public void Dispose()
        {

        }

        private const string DefaultLogPattern = "[%date][%level] %message";

        public LoggingLevel[] LoggingLevels { get; set; }
        public long Queued { get { return 0; } }
        public string LogPattern { get; set; }
        public string Parameters { get; set; }
        public event Logger.LogWritten OnLogWritten;
    }
}