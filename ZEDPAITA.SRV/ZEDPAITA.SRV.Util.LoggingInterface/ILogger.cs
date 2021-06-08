using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEDPAITA.SRV.Util.LoggingInterface
{
    public interface ILogger
    {
        /// <summary>
        /// Writes a message to the log
        /// </summary>
        /// <param name="category">A String of the category to write to</param>
        /// <param name="level">A LogLevel value of the level of this message</param>
        /// <param name="message">A String of the message to write to the log</param>
        void WriteMessage(string category, LogLevel level, string message, Exception exception=null);
    }
}
