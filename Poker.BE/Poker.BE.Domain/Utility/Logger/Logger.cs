using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Poker.BE.Domain.Utility.Logger
{
    /// <summary>
    /// static eager creation of singleton for Logger.
    /// CSV Style-like implementation.
    /// CSV - a file that you can open easily with MS Excel.
    /// </summary>
    /// <see cref="http://csharpindepth.com/Articles/General/Singleton.aspx"/>
    public sealed class Logger : ILogger
    {
        #region Fields
        private static readonly Logger instance = new Logger();
        private string filetype = default(string);
        private string filename = default(string);
        private string dirPath = default(string);
        private string delimiter = default(string);
        private string endl = default(string);
        #endregion

        #region Constructors
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Logger()
        {
        }

        /// <summary>
        /// this constructor build the logger as logging into csv file
        /// </summary>
        private Logger()
        {
            filetype = "csv";
            filename = "log." + filetype;
            dirPath = "";
            delimiter = ",";
            endl = "; ";
        }

        public static Logger Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        #region Properties
        public string[] LogMemory
        {
            get
            {
                return ReadFromFile();
            }
            set
            {
                OverwriteFile(value);
            }
        }
        #endregion

        #region Private Functions
        private void OverwriteFile(string message)
        {
            try
            {
                System.IO.File.WriteAllText(dirPath + filename, message);
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
            }
        }

        private void OverwriteFile(string[] message)
        {
            try
            {
                System.IO.File.WriteAllLines(dirPath + filename, message);
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
            }
        }

        private void AppendToFile(string message)
        {
            try
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(dirPath + filename, true))
                {
                    file.Write(message);
                    file.Write("\n");
                }
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
            }
        }

        private void AppendToFile(string[] message)
        {
            try
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(dirPath + filename, true))
                {
                    foreach (string str in message)
                    {
                        file.Write(message);
                    }
                    file.Write("\n");
                }
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
            }
        }

        private string[] ReadFromFile()
        {
            try
            {
                return System.IO.File.ReadAllLines(dirPath + filename);
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
                return null;
            }
        }

        //made this public for easy testing
        public string LogSuffix()
        {
            return "";
        }

        public string LogPrefix(string name, object sender, string priority)
        {
            return
                "[" + DateTime.Now.ToShortDateString() + " "
                + DateTime.Now.ToLongTimeString() + "]" + delimiter
                + priority + delimiter
                + name + delimiter
                + "<" + sender.GetType().FullName + "> :" + delimiter;
        }
        #endregion

        #region Methods

        public void Debug(string message, object sender, string priority = "Low")
        {
            AppendToFile(
                LogPrefix("Debug", sender, priority)
                + message +
                LogSuffix()
                );
        }

        public void Log(object obj, object sender, string priority = "Low")
        {
            AppendToFile(
               LogPrefix("Log", sender, priority)
               + obj.ToString() +
               LogSuffix()
               );
        }

        public void Log(string message, object sender, string priority = "Low")
        {
            AppendToFile(
               LogPrefix("Logging", sender, priority)
               + message +
               LogSuffix()
               );
        }

        public void Warn(string message, object sender, string priority = "Medium")
        {
            message = message.Replace("\n", endl);
            AppendToFile(
               LogPrefix("Warning", sender, priority)
               + message +
               LogSuffix()
               );
        }

        public void Error(string message, object sender, string priority = "High")
        {
            message = message.Replace("\n", endl);
            AppendToFile(
               LogPrefix("Error", sender, priority)
               + message +
               LogSuffix()
               );
        }

        public void Error(Exception e, object sender, string priority = "High")
        {
            AppendToFile(
               LogPrefix("Exception Error", sender, priority)
               + "Message: " + e.Message + endl
               + "Source: " + e.Source + endl
               + "Help Link: " + e.HelpLink
               + LogSuffix()
               );
        }

        public void Error(Exception e, string message, object sender, string priority = "High")
        {
            message = message.Replace("\n", endl);
            AppendToFile(
               LogPrefix("Exception Error", sender, priority)
               + "Our Message: " + message + endl
               + "Message: " + e.Message + endl
               + "Source: " + e.Source + endl
               + "Help Link: " + e.HelpLink
               + LogSuffix()
               );
        }

        public void Info(string message, object sender, string priority = "Low")
        {
            message = message.Replace("\n", endl);
            AppendToFile(
               LogPrefix("Info", sender, priority)
               + message +
               LogSuffix()
               );
        }


        #endregion
    }
}
