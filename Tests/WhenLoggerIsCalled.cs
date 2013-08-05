using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLogger;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WhenLoggerLogIsCalledWithLoggingLevelSetToDebug
    {
        private ILogger _logger;

        #region Setups and Teardowns

        [SetUp]
        public void SetUp()
        {
            _logger = new Logger
                {
                    DefaultLoggingLevel = LoggingLevel.Info,
                    Root = {LoggingLevels = new[] {LoggingLevel.Debug}}
                };
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

        [Test]
        public void LoggerQueueShouldNotBeIncreasedWithUnspecifiedLogLevel()
        {
            _logger.Log("Logging a test case");
            Assert.That(_logger.Queued, Is.EqualTo(0));
        }

        #endregion

    }
}
