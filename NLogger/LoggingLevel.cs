using System;

namespace NLogger
{
    [Flags]
    public enum LoggingLevel : byte
    {
        Error = 1 << 1,
        Warn = 1 << 2,
        Info = 1 << 3,
        Debug = 1 << 4,
        Trace = 1 << 5
    }
}