using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NLogger;
using NLogger.Appenders;
using NLogger.Configuration;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class WhenLoggerInitializeIsCalled
    {
        private ILogger _logger;

        #region Configuration file string

        private const string Configuration =
@"<NLoggerConfiguration>
  <root>
    <level error=""true""/>
  </root>
  <appender name=""FileAppender"" type=""NLogger.Appenders.FileLoggerAppender"" parameters=""C:\BS\log.txt"">
    <level info=""true""/>
    <pattern value=""%date %level %message""/>
  </appender>
  <appender name=""MemoryAppender"" type=""NLogger.Appenders.MemoryLoggerAppender"">
    <level debug=""true""/>
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
        public void LoggerShouldHaveTwoAppenders()
        {
            Assert.That(_logger.Appenders.Count, Is.EqualTo(2));
        }

        [Test]
        public void LoggerFileAppenderPatternShouldBeCorrect()
        {
            Assert.That(_logger.Appenders.First(x => x.GetType() == typeof(FileLoggerAppender)).LogPattern, Is.EqualTo("%date %level %message"));
        }
    }
}
