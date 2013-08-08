using System;
using System.Collections.Generic;
using System.IO;
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
    public class WhenFileAppenderLoggerIsCalledWithAttachedCustomOutput
    {
        private ILogger _logger;
        private List<string> _items;
        private bool _running;

        [SetUp]
        public void Setup()
        {
            _logger = new Logger();
            _logger.Initialize();
            _items = new List<string>();
            _running = true;
            var logger = _logger.Appenders.First(x => x.GetType() == typeof (FileLoggerAppender));
            logger.OnLogWritten += OnLogWritten;
        }

        public void OnLogWritten(IEnumerable<LogItem> logitems)
        {
            foreach (var item in logitems) 
                _items.Add(item.Message);

            _running = false;
        }

        // Hack for asynchronous testing
        private void Wait()
        {
            while (_running)
            {
                Thread.Sleep(50);
            }
        }


        [Test]
        public void CustomOutputShouldReceiveLoggedData()
        {
            for (int i = 0; i < 150; i++)
            {
                _logger.LogInfo("Logging message " + i, new Exception("Hello exception"));
            }


            Wait();
            Assert.That(_items.Count, Is.GreaterThan(10));
        }
    }
}
