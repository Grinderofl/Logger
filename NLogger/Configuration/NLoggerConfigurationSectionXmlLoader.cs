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
            var settings = new XmlReaderSettings()
                {
                    CloseInput = true,
                    IgnoreWhitespace = true,
                    IgnoreComments = true,
                    IgnoreProcessingInstructions = true,
                    CheckCharacters = false,
                    ValidationType = ValidationType.None
                };

            using (var reader = new XmlTextReader(new StringReader(xml)))
            {
                DeserializeSection(reader);
            }
        }
    }
}
