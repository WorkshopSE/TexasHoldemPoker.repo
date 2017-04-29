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
    /// </summary>
    /// <see cref="http://csharpindepth.com/Articles/General/Singleton.aspx"/>
    public sealed class Logger : ILogger
    {
        #region Fields
        private static readonly Logger instance = new Logger();
        private string filename = default(string);
        private string dirPath = default(string);
        private string[] logMemory;
        #endregion

        #region Constructors
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Logger()
        {
        }

        private Logger()
        {
            filename = "log.txt";
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
                ReadFromFile();
                return logMemory;
            }
        }
        #endregion

        #region Private Functions
        private void WriteToFile(string message)
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

        private void WriteToFile(string[] message)
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

        private void ReadFromFile()
        {
            try
            {
                logMemory = System.IO.File.ReadAllLines(dirPath + filename);
            }
            catch (IOException e)
            {
                //Note: we don't know if we have console to that
                Console.WriteLine(e.Message + ":\n\n" + e.StackTrace);
            }
        }
        #endregion

        #region Methods
        public void debug(string message)
        {
            throw new NotImplementedException();
        }

        public void error(string message)
        {
            throw new NotImplementedException();
        }

        public void error(Exception e)
        {
            throw new NotImplementedException();
        }

        public void error(Exception e, string message)
        {
            throw new NotImplementedException();
        }

        public void info(string message)
        {
            throw new NotImplementedException();
        }

        public void Log(object obj)
        {
            throw new NotImplementedException();
        }

        public void Log(string message)
        {
            throw new NotImplementedException();
        }

        public void warn(string message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
