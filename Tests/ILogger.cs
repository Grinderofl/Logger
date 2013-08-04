using System.Collections.Generic;

namespace Tests
{
    public interface ILogger
    {
        Queue<string> Queue { get; set; }

        void Log(string message);
    }
}