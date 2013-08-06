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

        [ConfigurationProperty("file", IsRequired = false)]
        public NLoggerFile File
        {
            get { return (NLoggerFile) this["file"]; }
            set { this["file"] = value; }
        }

        [ConfigurationProperty("timesincelastwrite")]
        public string TimeSinceLastWrite
        {
            get { return (string) this["timesincelastwrite"]; }
            set { this["timesincelastwrite"] = value; }
        }

        [ConfigurationProperty("maxqueuesize")]
        public int MaxQueueSize
        {
            get { return (int) this["maxqueuesize"]; }
            set { this["maxqueuesize"] = value; }
        }

        [ConfigurationProperty("timebetweenchecks")]
        public int TimeBetweenChecks
        {
            get { return (int) this["timebetweenchecks"]; }
            set { this["timebetweenchecks"] = value; }
        }
    }
}