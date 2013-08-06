using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using System.Text;
using System.Threading;

namespace NLogger.Appenders
{
    public class FileLoggerAppender : ILogAppender
    {
        #region Fields

        private ConcurrentQueue<LogItem> _queue;

        private bool _disposing;

        private Thread _loggerThread;

        #endregion


        #region Constants

        private const string DefaultLogPattern = "[%level] %date %message | %exception %stacktrace";

        #endregion


        #region Properties

        public LoggingLevel[] LoggingLevels { get; set; }
        public long Queued { get { return _queue.Count; } }
        public string LogPattern { get; set; }
        public string Parameters { get; set; }
        public TimeSpan TimeSinceLastWrite { get; set; }

        #endregion


        #region Events

        public event Logger.LogWritten OnLogWritten;

        #endregion


        #region Constructors and destructors

        public FileLoggerAppender()
        {
            _queue = new ConcurrentQueue<LogItem>();
            OnLogWritten += DefaultLogWriter;
            BeginLogWriter();
        }

        #endregion


        #region ILogAppender method implementations

        public void Log(string message, Exception exception, LoggingLevel level)
        {
            _queue.Enqueue(new LogItem(message, exception, level));
        }

        #endregion


        #region IDisposable implementation

        public void Dispose()
        {
            _disposing = true;
            FinalizeDispose();
        }

        #endregion


        #region Private methods

        #region Alternate implementation

        private void BeginLogWriter()
        {
            _loggerThread = new Thread(OnThreadStart);
            _loggerThread.Start();
            TimeSinceLastWrite = new TimeSpan(0, 0, 30);
        }

        private void OnThreadStart()
        {
            do
            {
                Thread.Sleep(500);
                if (_queue.Count < 100) continue;
                if (OnLogWritten == null) continue;
                var logItems = new List<LogItem>();
                for (var i = 0; i < _queue.Count; i++)
                {
                    LogItem item;
                    while (!_queue.TryDequeue(out item))
                        Thread.Sleep(50);
                    logItems.Add(item);
                }
                OnLogWritten(logItems);
            } while (!_disposing);
            _loggerThread.Abort();
            _loggerThread = null;
        }

        #endregion

        private void DefaultLogWriter(IList<LogItem> logItems)
        {
            try
            {
                using (
                    var fs = new FileStream(Parameters, FileMode.Append, FileAccess.Write, FileShare.Write, 256,
                                            FileOptions.WriteThrough))
                {
                    using (var fw = new StreamWriter(fs, new UTF8Encoding(), 256, true))
                        foreach (var item in logItems)
                        {
                            var toWrite = string.Format("{0}",
                                                        Logger.FormatLog(
                                                            string.IsNullOrEmpty(LogPattern)
                                                                ? DefaultLogPattern
                                                                : LogPattern, item));
                            fw.WriteLine(toWrite);
                        }
                    fs.Flush(true);
                }
            }
            catch (IOException e)
            {
                if (!IsFileLocked(e))
                    throw;
                Thread.Sleep(2000);
                DefaultLogWriter(logItems);
            }
        }

        private void FinalizeDispose()
        {
            _queue = null;
        }

        private static bool IsFileLocked(IOException exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == 32 || errorCode == 33;
        }

        #endregion

    }
}
