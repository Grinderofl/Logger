using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WhenLoggerLogIsCalled
    {
        [Test]
        public void LoggerQueueShouldBeIncreased()
        {
            ILogger logger = new Logger();
            logger.Log("Logging a test case");
            Assert.That(logger.Queued, Is.EqualTo(1));
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

        private readonly Queue<string> _queue;

        #endregion

        #region Properties

        public long Queued { get { return _queue.Count; } }

        #endregion

        #region Implemented methods

        public void Log(string message)
        {
            _queue.Enqueue(message);
        }

        #endregion

        #region Private methods

        #endregion
    }
}
