using NLogger;
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
            _logger = new Logger { LogLevels = LoggingLevel.Debug };
            var fileLogger = new FileLoggerAppender {LogLevels = LoggingLevel.Error};
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
        public void FileSizeIncreasesWhenLoggingIsPerformed()
        {
            
        }

        #endregion

    }
}