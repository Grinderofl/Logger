using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NLogger.Configuration
{
    public sealed class NLoggerConfigurationSectionXmlLoader : NLoggerConfigurationSection
    {
        public NLoggerConfigurationSectionXmlLoader(string xml)
        {
            using (var reader = new XmlTextReader(new StringReader(xml)))
            {
                DeserializeSection(reader);
            }
        }
    }
}
