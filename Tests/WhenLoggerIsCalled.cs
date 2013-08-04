using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WhenLoggerLogIsCalledWithLoggingLevelSetToDebug
    {
        private ILogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new Logger {LogLevels = LoggingLevel.Debug};
        }

        [TearDown]
        public void Teardown()
        {
            _logger.Dispose();
            _logger = null;
        }

        [Test]
        public void LoggerQueueShouldBeIncreasedWithLogLevelSetToDebug()
        {
            _logger.Log("Logging a test case", LoggingLevel.Debug);
            Assert.That(_logger.Queued, Is.EqualTo(1));
        }

        [Test]
        public void LoggerQueueShouldNotBeIncreasedWithLogLevelSetToInfo()
        {
            _logger.Log("Logging a test case", LoggingLevel.Info);
            Assert.That(_logger.Queued, Is.EqualTo(0));
        }
    }

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
        public long Queued { get { return _queue.Count; } }

        #endregion


        #region ILogger Implemented methods

        public void Log(string message, LoggingLevel level)
        {
            _queue.Enqueue(message);
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
