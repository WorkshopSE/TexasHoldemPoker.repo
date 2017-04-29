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
        void Log(object obj, object sender, string priority = "Low");
        void Log(string message, object sender, string priority = "Low");
        void Warn(string message, object sender, string priority = "Medium");
        void Error(string message, object sender, string priority = "High");
        void Error(Exception e, object sender, string priority = "High");
        void Error(Exception e, string message, object sender, string priority = "High");
        void Debug(string message, object sender, string priority = "Low");
        void Info(string message, object sender, string priority = "Low");

    }
}
