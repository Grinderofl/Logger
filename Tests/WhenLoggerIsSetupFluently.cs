using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLogger;
using NLogger.Appenders;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WhenLoggerIsSetupFluently
    {
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = new Logger().Initialize(null, true);
            var appender = new FileLoggerAppender()
                {
                    Location = "C:\\logs\\log.log",
                    LoggingLevels = new[] {LoggingLevel.Debug, LoggingLevel.Error, LoggingLevel.Fatal},
                    MaxFileSize = "10000KB",
                    MaxLogCount = 5,
                    TimeBetweenChecks = 50,
                    TimeSinceLastWrite = new TimeSpan(0, 0, 0, 20),
                    Name = "FileLogger"
                };
            _logger.Appenders.Add(appender);

        }

        [Test]
        public void LoggerAppenderCountShouldBeCorrect()
        {
            Assert.That(_logger.Appenders.Count, Is.EqualTo(1));
        }
    }
}
