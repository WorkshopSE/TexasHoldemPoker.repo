using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Poker.BE.CrossUtility.Loggers
{
    /// <summary>
    /// static eager creation of singleton for Logger.
    /// CSV Style-like implementation.
    /// CSV - a file that you can open easily with MS Excel.
    /// </summary>
    /// <see cref="http://csharpindepth.com/Articles/General/Singleton.aspx"/>
    public sealed class Logger : ILogger
    {

        #region Constants
        public static readonly string ENDL = "; ";
        public static readonly string DELIMITER = ","; // CSV delimiter
        #endregion

        #region Fields
        private static readonly Logger instance = new Logger();
        private string filetype = default(string);
        private string filename = default(string);
        private string dirPath = default(string);
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
                lock (this)
                {
                    File.WriteAllText(dirPath + filename, message);
                }
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
            }
            catch (Exception)
            {
                // NOTE: do nothing at total fail, to avoid crash.
                return;
            }
        }

        private void OverwriteFile(string[] message)
        {
            try
            {
                lock (this)
                {
                    File.WriteAllLines(dirPath + filename, message); 
                }
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
            }
            catch (Exception)
            {
                // NOTE: do nothing at total fail, to avoid crash.
                return;
            }
        }

        private void AppendToFile(string message)
        {
            try
            {
                lock (this)
                {
                    using (StreamWriter file =
                                new StreamWriter(dirPath + filename, true))
                    {
                        file.Write(message);
                        file.Write("\n");
                    } 
                }
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
            }
            catch (Exception)
            {
                // NOTE: do nothing at total fail, to avoid crash.
                return;
            }
        }

        private void AppendToFile(string[] message)
        {
            try
            {
                lock (this)
                {
                    using (StreamWriter file =
                                new StreamWriter(dirPath + filename, true))
                    {
                        foreach (string str in message)
                        {
                            file.Write(message);
                        }
                        file.Write("\n");
                    } 
                }
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
            }
            catch (Exception)
            {
                // NOTE: do nothing at total fail, to avoid crash.
                return;
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
            catch (Exception)
            {
                // NOTE: do nothing at total fail, to avoid crash.
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
                + DateTime.Now.ToLongTimeString() + "]" + DELIMITER
                + priority + DELIMITER
                + name + DELIMITER
                + "<" + sender.GetType().FullName + "> :" + DELIMITER;
        }
        #endregion

        #region Methods

        public void Debug(string message, object sender, string priority = "Low")
        {
            message = message.Replace("\n", ENDL);
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
            message = message.Replace("\n", ENDL);
            AppendToFile(
               LogPrefix("Logging", sender, priority)
               + message +
               LogSuffix()
               );
        }

        public void Warn(string message, object sender, string priority = "Medium")
        {
            message = message.Replace("\n", ENDL);
            AppendToFile(
               LogPrefix("Warning", sender, priority)
               + message +
               LogSuffix()
               );
        }

        public void Error(string message, object sender, string priority = "High")
        {
            message = message.Replace("\n", ENDL);
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
               + "Message: " + e.Message + ENDL
               + "Source: " + e.Source + ENDL
               + "Help Link: " + e.HelpLink
               + LogSuffix()
               );
        }

        public void Error(Exception e, string message, object sender, string priority = "High")
        {
            message = message.Replace("\n", ENDL);
            AppendToFile(
               LogPrefix("Exception Error", sender, priority)
               + "Our Message: " + message + ENDL
               + "Message: " + e.Message + ENDL
               + "Source: " + e.Source + ENDL
               + "Help Link: " + e.HelpLink
               + LogSuffix()
               );
        }

        public void Info(string message, object sender, string priority = "Low")
        {
            message = message.Replace("\n", ENDL);
            AppendToFile(
               LogPrefix("Info", sender, priority)
               + message +
               LogSuffix()
               );
        }


        #endregion
    }
}
