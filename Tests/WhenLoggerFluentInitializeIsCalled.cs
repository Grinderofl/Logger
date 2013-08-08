using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLogger;
using NLogger.Appenders;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WhenLoggerFluentInitializeIsCalled
    {
        private ILogger _logger;
        private List<LogItem> _messages;
        private bool _waiting;
        

        [SetUp]
        public void Setup()
        {
            _waiting = true;
            _messages = new List<LogItem>();
            _logger =
                new Logger().InitializeFluent(new[] {LoggingLevel.Error})
                            .Appender(
                                new FileLoggerAppender("FileLogger", "%thread %date %message", timeBetweenChecks:500, timeSinceLastWrite:new TimeSpan(0, 0, 10),
                                                       location: "C:\\logs2\\mylog.log", maxQueueCache:50).AddLevel(LoggingLevel.Debug)
                                                                                        .AddLevel(LoggingLevel.Info)
                                                                                        .OnLogWritten(
                                                                                            items =>
                                                                                                {
                                                                                                    _messages
                                                                                                        .AddRange(
                                                                                                            items);
                                                                                                    _waiting = false;
                                                                                                }));
        }

        private void Wait()
        {
            while (_waiting)
            {
                Thread.Sleep(50);
            }
        }

        [Test]
        public void LoggerShouldHaveCorrectAmountOfAppenders()
        {
            Assert.That(_logger.Appenders.Count, Is.EqualTo(1));
        }

        [Test]
        public void LoggerFileAppenderShouldReceiveMessages()
        {
            for (int i = 0; i < 150; i++)
            {
                _logger.LogDebug("Debug message");
            }

            Wait();
            Assert.That(_messages.Count, Is.GreaterThan(1));
        }

    }
}
