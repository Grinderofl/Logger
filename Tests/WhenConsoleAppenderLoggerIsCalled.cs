using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLogger;
using NLogger.Configuration;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Tests.Helpers;

namespace Tests
{
    [TestFixture]
    public class WhenConsoleAppenderLoggerIsCalled
    {
        private ILogger _logger;

        #region Configuration file string

        private const string Configuration =
@"<NLoggerConfiguration>
  <root>
    <level error=""true""/>
  </root>
  <appender name=""ConsoleAppender"" type=""NLogger.Appenders.ConsoleLoggerAppender, NLogger"" parameters=""C:\BS\log.txt"">
    <level info=""true""/>
    <pattern value=""%date %level %message""/>
  </appender>
</NLoggerConfiguration>";

        #endregion

        [SetUp]
        public void Setup()
        {
            _logger = new Logger();
            _logger.Initialize(new NLoggerConfigurationSectionXmlLoader(Configuration));
        }

        [Test]
        public void ConsoleOutputShouldIncrease()
        {
            var current = Console.Out;
            const string text = "Outputting info";
            using (var output = new ConsoleOutput())
            {
                _logger.LogInfo(text);
                Assert.That(output.GetOutput(), Is.StringContaining(text));
            }
            Assert.That(Console.Out, Is.EqualTo(current));
        }
    }
}
