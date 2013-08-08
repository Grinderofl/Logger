using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// <summary>
    /// File Logger Appender
    /// </summary>
    public class FileLoggerAppender : LogAppender
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

        /*public string Name { get; set; }
        public LoggingLevel[] LoggingLevels { get; set; }
        
        public string LogPattern { get; set; }
        public string Parameters { get; set; }
        public TimeSpan TimeSinceLastWrite { get; set; }
        public int MaxQueueCache { get; set; }
        public int TimeBetweenChecks { get; set; }
        public string MaxFileSize { get; set; }
        public string Location { get; set; }
        public int MaxLogCount { get; set; }*/

        public new long Queued { get { return _queue.Count; } }

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
        }

        public FileLoggerAppender(string name, string pattern = "", string parameters = "",
                                  TimeSpan timeSinceLastWrite = new TimeSpan(), int maxQueueCache = 100,
                                  int timeBetweenChecks = 50, string maxFileSize = "10MB", string location = "",
                                  int maxLogCount = 0)
            : base(
                name, pattern, parameters, timeSinceLastWrite, maxQueueCache, timeBetweenChecks, maxFileSize, location,
                maxLogCount)
        {
            _queue = new ConcurrentQueue<LogItem>();
            OnLogWritten += DefaultLogWriter;
            BeginLogWriter();
        }

        #endregion


        #region ILogAppender method implementations

        public override void Log(string message, Exception exception, LoggingLevel level)
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
                if ((_queue.Count < MaxQueueCache && (DateTime.Now - _lastWrite) < TimeSinceLastWrite) || _queue.Count == 0) continue;
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



        private new void DefaultLogWriter(IList<LogItem> logItems)
        {
            base.DefaultLogWriter(logItems);
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
                                if(directory != null)
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
                
            }

            try
            {
                if (logItems.Count == 0) return;
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
                    throw;
                Thread.Sleep(2000);
                DefaultLogWriter(logItems);
            }
            catch (ArgumentNullException)
            {
                
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
