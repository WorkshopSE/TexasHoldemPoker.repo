using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility.Logger
{
    public interface ILogger
    {
        /* Note:
         * Every function should deliver:
         * time stamp
         * severity / priority - type & color of message
         * the message
         * the current thread & thread info (task, Process, etc.)
         * Stack Trace (if needed!, only squashed & little of it)
         * current class / function / relevant fields / properties
         * */
        void Log(object obj);
        void Log(string message);
        void warn(string message);
        void error(string message);
        void error(Exception e);
        void error(Exception e, string message);
        void debug(string message);
        void info(string message);

    }
}
