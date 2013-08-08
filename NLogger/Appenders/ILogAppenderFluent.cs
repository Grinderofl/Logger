using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger.Appenders
{
    public interface ILogAppenderFluent : ILogAppender
    {
        /// <summary>
        /// Adds a level to log appender
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        ILogAppenderFluent AddLevel(LoggingLevel level);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pattern"></param>
        /// <param name="parameters"></param>
        /// <param name="timeSinceLastWrite"></param>
        /// <param name="maxQueueCache"></param>
        /// <param name="timeBetweenChecks"></param>
        /// <param name="maxFileSize"></param>
        /// <param name="location"></param>
        /// <param name="maxLogCount"></param>
        /// <returns></returns>
        //ILogAppenderFluent LogAppenderFluent(string name, string pattern = "", string parameters = "", TimeSpan timeSinceLastWrite = default(TimeSpan), int maxQueueCache = 0, int timeBetweenChecks = 50, string maxFileSize = "10MB", string location = "", int maxLogCount = 0);

        /// <summary>
        /// Adds an action to log written event
        /// </summary>
        /// <param name="logWritten"></param>
        /// <returns></returns>
        ILogAppenderFluent OnLogWritten(Logger.LogWritten logWritten);

    }
}
