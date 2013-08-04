using System.Collections.Generic;

namespace Tests
{
    public interface ILogger
    {
        long Queued { get; }

        void Log(string message);
    }
}