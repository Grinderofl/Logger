using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger.Configuration
{
    public class NLoggerFile : ConfigurationElement
    {
        [ConfigurationProperty("location")]
        public string Location
        {
            get { return (string) this["location"]; }
            set { this["location"] = value; }
        }

        [ConfigurationProperty("maxsize")]
        public string MaxSize
        {
            get { return (string) this["maxsize"]; }
            set { this["maxsize"] = value; }
        }

        [ConfigurationProperty("maxcount")]
        public int MaxCount
        {
            get { return (int) this["maxcount"]; }
            set { this["maxcount"] = value; }
        }
    }
}
