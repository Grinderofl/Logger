using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace NLogger.Appenders
{
    public class FileLoggerAppender : ILogAppender
    {
        #region Fields

        private ConcurrentQueue<LogItem> _queue;

        private readonly Dictionary<string, Func<LogItem, string>> _formatting = new Dictionary
    <string, Func<LogItem, string>>()
            {
                {"%exception", x => x.Exception != null ? x.Exception.Message.Replace(Environment.NewLine, "") : ""},
                {
                    "%stacktrace",
                    x =>
                    x.Exception != null && x.Exception.StackTrace != null
                        ? x.Exception.StackTrace.Replace(Environment.NewLine, "")
                        : ""
                }
            };

        private readonly Dictionary<string, long> _conversion = new Dictionary<string, long>
            {
                {"KB", 1024},
                {"MB", 1024*1024},
                {"GB", 1024*1024*1024}
            };

        private bool _disposing;

        private Thread _loggerThread;

        private DateTime _lastWrite;

        #endregion


        #region Constants

        private const string DefaultLogPattern = "[%level] %date %message | %exception %stacktrace";

        #endregion


        #region Properties

        public string Name { get; set; }
        public LoggingLevel[] LoggingLevels { get; set; }
        public long Queued { get { return _queue.Count; } }
        public string LogPattern { get; set; }
        public string Parameters { get; set; }
        public TimeSpan TimeSinceLastWrite { get; set; }
        public int MaxQueueCache { get; set; }
        public int TimeBetweenChecks { get; set; }
        public string MaxFileSize { get; set; }
        public string Location { get; set; }
        public int MaxLogCount { get; set; }

        #endregion


        #region Events

        public event Logger.LogWritten OnLogWritten;

        #endregion


        #region Constructors and destructors

        public FileLoggerAppender()
        {
            MaxLogCount = -1;
            TimeSinceLastWrite = new TimeSpan(0, 0, 30);
            TimeBetweenChecks = 30;
            MaxQueueCache = 100;
            _queue = new ConcurrentQueue<LogItem>();
            OnLogWritten += DefaultLogWriter;
            BeginLogWriter();
            EventLogWriter.Log("FileLoggerAppender created", EventLogEntryType.Information, 0);
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
            _lastWrite = DateTime.Now;
        }

        private void OnThreadStart()
        {
            do
            {
                Thread.Sleep(TimeBetweenChecks);
                if (_queue.Count < MaxQueueCache && (DateTime.Now - _lastWrite) < TimeSinceLastWrite) continue;
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
                _lastWrite = DateTime.Now;
            } while (!_disposing);
            _loggerThread.Abort();
            _loggerThread = null;
            FinalizeDispose();
        }

        #endregion



        private void DefaultLogWriter(IList<LogItem> logItems)
        {
            
            if (!string.IsNullOrWhiteSpace(MaxFileSize))
            {
                var result = Regex.Match(MaxFileSize, @"\d+").Value;
                long size;
                if (long.TryParse(result, out size))
                {
                    for (var i = 0; i < _conversion.Count; i++)
                    {
                        var element = _conversion.ElementAt(i);
                        if (!MaxFileSize.ToUpper().Contains(element.Key)) continue;
                        size *= element.Value;
                        break;
                    }
                    try
                    {
                        if (File.Exists(Location))
                        {
                            var info = new FileInfo(Location);
                            if (info.Length >= size)
                            {
                                File.SetLastWriteTime(Location, DateTime.Now);
                                var move = DateTime.Now;
                                info.MoveTo(Location + "." + move.ToString("yyyy/dd/MM_HH-mm-ss.fffffff"));
                                if (MaxLogCount != -1)
                                {
                                    var directory = Path.GetDirectoryName(Location);
                                    var filename = Path.GetFileName(Location);
                                    if (directory != null)
                                    {
                                        var files = Directory.GetFiles(directory, filename + ".*",
                                                                       SearchOption.TopDirectoryOnly);
                                        if (MaxLogCount == 0)
                                            files.ForEach(File.Delete);
                                        else
                                        {
                                            files.Select(x => new FileInfo(x))
                                                 .ToList()
                                                 .OrderByDescending(x => x.LastWriteTime)
                                                 .Skip(MaxLogCount).ForEach(x => File.Delete(x.FullName));
                                        }

                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        EventLogWriter.Log(
                            string.Format(
                                "Exception occurred in FileLoggerAppender -> DefaultLogWriter, {2}Message: {2}{0}{2}StackTrace: {2}{1}{2}Source: {2}{3}",
                                e.Message, e.StackTrace, Environment.NewLine, e.Source), EventLogEntryType.Error, 4);
                    }
                }
                
            }

            try
            {
                if (string.IsNullOrWhiteSpace(Location)) return;
                Debug.Assert(Location != null, "Location is null");
                if (!Directory.Exists(Path.GetDirectoryName(Location)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Location));
                }
                using (
                    var fs = new FileStream(Location, FileMode.Append, FileAccess.Write, FileShare.Write, 256,
                                            FileOptions.WriteThrough))
                {
                    using (var fw = new StreamWriter(fs, new UTF8Encoding(), 256, true))
// ReSharper disable ForCanBeConvertedToForeach Reason: Optimization
                        for (int i = 0; i < logItems.Count; i++)
// ReSharper restore ForCanBeConvertedToForeach
                        {
                            var toWrite = string.Format("{0}",
                                                        Logger.FormatLog(
                                                            string.IsNullOrEmpty(LogPattern)
                                                                ? DefaultLogPattern
                                                                : LogPattern, logItems[i], _formatting));
                            fw.WriteLine(toWrite);
                        }
                    fs.Flush(true);
                }
            }
            catch (IOException e)
            {
                if (!IsFileLocked(e))
                {
                    EventLogWriter.Log(
                        string.Format(
                            "IOException occurred in FileLoggerAppender -> DefaultLogWriter, {2}Message: {2}{0}{2}StackTrace: {2}{1}{2}Source: {2}{3}",
                                e.Message, e.StackTrace, Environment.NewLine, e.Source), EventLogEntryType.Error, 3);
                    return;
                }
                Thread.Sleep(2000);
                DefaultLogWriter(logItems);
            }
            catch (ArgumentNullException e)
            {
                EventLogWriter.Log(
                    string.Format(
                        "ArgumentNullException occurred in FileLoggerAppender -> DefaultLogWriter, {2}Message: {2}{0}{2}StackTrace: {2}{1}{2}Source: {2}{3}",
                                e.Message, e.StackTrace, Environment.NewLine, e.Source), EventLogEntryType.Error, 1);
                // Sometimes a 'file not found' exception is thrown, not sure why
            }
            catch (Exception e)
            {
                EventLogWriter.Log(
                    string.Format(
                        "Exception occurred in FileLoggerAppender -> DefaultLogWriter, {2}Message: {2}{0}{2}StackTrace: {2}{1}{2}Source: {2}{3}",
                                e.Message, e.StackTrace, Environment.NewLine, e.Source), EventLogEntryType.Error, 2);
            }
        }

        private void FinalizeDispose()
        {
            _queue = null;
        }

        private static bool IsFileLocked(Exception exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == 32 || errorCode == 33;
        }

        #endregion

    }
}
