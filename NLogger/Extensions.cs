using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger
{
    public static class LoggerExtensions
    {
        public static bool Contains(this LoggingLevel[] levels, LoggingLevel level)
        {
            if(levels.Length == 0) return false;
            for (var i = 0; i < levels.Length; i++)
                if (levels[i] == level) return true;

            return false;
        }
    }

}
