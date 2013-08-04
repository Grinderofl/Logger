using System.Configuration;

namespace NLogger.Configuration
{
    public class NLoggerAppender : RootAppender
    {
        /// <summary>
        /// Appender name
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Appender type
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string) this["type"]; }
            set { this["type"] = value; }
        }

        /// <summary>
        /// Appender parameters
        /// </summary>
        [ConfigurationProperty("parameters", IsRequired = false)]
        public string Parameters
        {
            get { return (string) this["parameters"]; }
            set { this["parameters"] = value; }
        }
    }
}