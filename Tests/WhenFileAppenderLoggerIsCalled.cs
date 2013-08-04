using System.Linq;
using NLogger;
using NLogger.Appenders;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WhenFileAppenderLoggerIsCalled
    {
        private ILogger _logger;

        #region Setups and Teardowns

        [SetUp]
        public void SetUp()
        {
            _logger = new Logger {DefaultLoggingLevel = LoggingLevel.Info};
            _logger.Root.LoggingLevels = new[] { LoggingLevel.Debug };
            var fileLogger = new FileLoggerAppender {LoggingLevels = new[] {LoggingLevel.Error}};
            _logger.Appenders.Add(fileLogger);
        }

        [TearDown]
        public void Teardown()
        {
            _logger.Dispose();
            _logger = null;
        }

        #endregion


        #region Tests

        [Test]
        public void FileAppenderQueueSizeIncreases()
        {
            _logger.Log("Appending a message", LoggingLevel.Error);
            Assert.That(_logger.Appenders.First(x => x.GetType() == typeof(FileLoggerAppender)).Queued, Is.EqualTo(1));
        }
        
        #endregion

    }
}