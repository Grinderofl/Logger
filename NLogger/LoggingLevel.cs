using System;
using System.ComponentModel;

namespace NLogger
{
    public enum LoggingLevel
    {
        [Description("Fatal")]
        Fatal,

        [Description("Error")]
        Error,

        [Description("Warning")]
        Warning,

        [Description("Info")]
        Info,

        [Description("Debug")]
        Debug,

        [Description("Trace")]
        Trace
    }
}