using System.Configuration;

namespace NLogger.Configuration
{
    public class NLoggerAppenderLevel : ConfigurationElement
    {
        [ConfigurationProperty("fatal")]
        public bool Fatal
        {
            get { return (bool) this["fatal"]; }
            set { this["fatal"] = value; }
        }

        [ConfigurationProperty("error")]
        public bool Error
        {
            get { return (bool) this["error"]; }
            set { this["error"] = value; }
        }

        [ConfigurationProperty("warning")]
        public bool Warning
        {
            get { return (bool)this["warning"]; }
            set { this["warning"] = value; }
        }

        [ConfigurationProperty("info")]
        public bool Info
        {
            get { return (bool)this["info"]; }
            set { this["info"] = value; }
        }

        [ConfigurationProperty("debug")]
        public bool Debug
        {
            get { return (bool)this["debug"]; }
            set { this["debug"] = value; }
        }

        [ConfigurationProperty("trace")]
        public bool Trace
        {
            get { return (bool)this["trace"]; }
            set { this["trace"] = value; }
        }
    }
}