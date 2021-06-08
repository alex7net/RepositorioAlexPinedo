using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using ZEDPAITA.SRV.Util.LoggingInterface;

namespace ZEDPAITA.SRV.Util.Log4Net
{
    /// <summary>
    /// Driver class to adapts calls from ILogger to work with a log4net backend
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        public Log4NetLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        /// <summary>
        /// Writes messages to the log4net backend.
        /// </summary>
        /// <remarks>
        /// This method is responsible for converting the WriteMessage call of
        /// the interface into something log4net can understand.  It does this
        /// by doing a switch/case on the log level and then calling the 
        /// appropriate log method
        /// </remarks>
        /// <param name="category">A string of the category to log to</param>
        /// <param name="level">A LogLevel value of the level of the log</param>
        /// <param name="message">A String of the message to write to the log</param>
        public void WriteMessage(string category, LogLevel level, string message, Exception exception= null)
        {
            // Get the Log we are going to write this message to            
            ILog log = LogManager.GetLogger(category);

            switch (level)
            {
                case LogLevel.FATAL:
                    if (log.IsFatalEnabled) log.Fatal(message);
                    break;
                case LogLevel.ERROR:
                    if (log.IsErrorEnabled) log.Error(message, exception);
                    break;
                case LogLevel.WARN:
                    if (log.IsWarnEnabled) log.Warn(message);
                    break;
                case LogLevel.INFO:
                    if (log.IsInfoEnabled) log.Info(message);
                    break;
                case LogLevel.VERBOSE:
                    if (log.IsDebugEnabled) log.Debug(message);
                    break;
            }
        }
    }
}
