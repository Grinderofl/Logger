using System.Configuration;

namespace NLogger.Configuration
{
    public class NLoggerConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public NLoggerAppenderCollection Appenders
        {
            get { return (NLoggerAppenderCollection) base[""]; }
        }

        [ConfigurationProperty("root")]
        public RootAppender Root
        {
            get { return (RootAppender) base["root"]; }
        }
    }
}
