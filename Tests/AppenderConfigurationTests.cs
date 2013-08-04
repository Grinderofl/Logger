using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLogger.Configuration;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AppenderConfigurationTests
    {
        [Test]
        public void AppenderConfigurationSectionCanBeFound()
        {
            var t = ConfigurationManager.GetSection("NLoggerConfiguration");
            NLoggerConfigurationSection config =
                 ConfigurationManager.GetSection("NLoggerConfiguration") as NLoggerConfigurationSection;
            var level = config.Appenders;
            foreach (var item in config.Appenders)
            {
                
            }
            Assert.That(config, Is.Not.Null);
        }
    }
}
