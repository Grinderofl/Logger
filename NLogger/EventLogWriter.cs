using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger
{
    internal static class EventLogWriter
    {
        private const string Source = "NLogger";
        private const string LogName = "Application";
        private static object _lock = new object();

        public static void Log(string message, EventLogEntryType type, int id)
        {
            /*if(!EventLog.SourceExists(Source))
                EventLog.CreateEventSource(Source, LogName);

            EventLog.WriteEntry(Source, message, type, id);*/
            lock (_lock)
            {
                File.AppendAllText("NLoggerErrors.log",
                                   string.Format("[{0}] {1} | {4} | {2}{3}", type, id, message, Environment.NewLine,
                                                 DateTime.Now));
            }
        }

    }
}
