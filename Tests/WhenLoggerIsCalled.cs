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
        public void LoggerQueueShouldBeExtended()
        {
            ILogger logger = new Logger();
            logger.Log("Logging a test case");
            Assert.That(logger.Queue.Count, Is.GreaterThan(0));
        }
    }

    public interface ILogger
    {
        Queue<string> Queue { get; set; }

        void Log(string message);
    }
}
