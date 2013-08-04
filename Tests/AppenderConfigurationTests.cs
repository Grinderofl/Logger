using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
            var config = ConfigurationManager.GetSection("NLoggerConfiguration") as NLoggerConfigurationSection;

            Debug.Assert(config != null, "config != null");
            foreach (var item in config.Appenders)
            {
                
            }
            Assert.That(config, Is.Not.Null);
        }
    }
}
