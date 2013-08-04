using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace NLogger.Configuration
{
    public class NLoggerConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public NLoggerAppenderCollection Appenders
        {
            get { return (NLoggerAppenderCollection) base[""]; }
        }

        /*public const string SectionName = "NLoggerConfiguration";

        private const string AppendersName = "Appenders";

        [ConfigurationProperty(AppendersName)]
        [ConfigurationCollection(typeof(NLoggerAppender), AddItemName = "appender")]
        public NLoggerAppenderCollection Appenders { get { return (NLoggerAppenderCollection) base[AppendersName]; } }

        public IEnumerable<NLoggerAppender> Appenders2 { get { foreach (var item in Appenders) yield return (NLoggerAppender) item; } }
        */
    }

    public class NLoggerAppenderCollection : ConfigurationElementCollection
    {
        private const string CollectionElementName = "appender";

        public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMap; } }

        protected override string ElementName { get { return CollectionElementName; } }

        protected override ConfigurationElement CreateNewElement()
        {
            return new NLoggerAppender();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NLoggerAppender) element).Name;
        }
    }

    /*[ConfigurationCollection(typeof(NLoggerAppender), AddItemName = "appender")]
    public class NLoggerAppenderCollection : ConfigurationElementCollection, IList
    {
        public NLoggerAppenderCollection()
        {
            var details = (NLoggerAppender) CreateNewElement();
            if (details.Type != "")
                Add(details);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new NLoggerAppender();
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NLoggerAppender) element).Name;
        }

        public NLoggerAppender this[int index]
        {
            get { return (NLoggerAppender) BaseGet(index); }
            set
            {
                if(BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        public bool IsReadOnly { get; private set; }
        public bool IsFixedSize { get; private set; }

        public int IndexOf(NLoggerAppender details)
        {
            return BaseIndexOf(details);
        }

        public void Add(NLoggerAppender details)
        {
            BaseAdd(details);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(NLoggerAppender details)
        {
            if(BaseIndexOf(details) >= 0)
                BaseRemove(details.Name);
        }

        public void Remove(object value)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        object IList.this[int index]
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public new IEnumerator GetEnumerator()
        {
            return this.OfType<NLoggerAppenderCollection>().GetEnumerator();
        }

        public int Add(object value)
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            BaseClear();
        }

        public int IndexOf(object value)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new System.NotImplementedException();
        }

        protected override string ElementName
        {
            get { return "appender"; }
        }
    }
    */
    public class NLoggerAppender : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string) this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string) this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("parameters", IsRequired = false)]
        public string Parameters
        {
            get { return (string) this["parameters"]; }
            set { this["parameters"] = value; }
        }

        [ConfigurationProperty("level")]
        public NLoggerAppenderLevel Level
        {
            get { return (NLoggerAppenderLevel) this["level"]; }
            set { this["level"] = value; }
        }

        [ConfigurationProperty("pattern")]
        public NLoggerAppenderPattern Pattern
        {
            get { return (NLoggerAppenderPattern)this["pattern"]; }
            set { this["pattern"] = value; }
        }
    }

    public class NLoggerAppenderLevel : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string) this["value"]; }
            set { this["value"] = value; }
        }
    }

    public class NLoggerAppenderPattern : ConfigurationElement
    {
        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
    }
}
