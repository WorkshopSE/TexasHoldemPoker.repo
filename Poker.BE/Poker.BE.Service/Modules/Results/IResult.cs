using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
    public abstract class IResult
    {
        /// <summary>
        /// error message log with accumulate set (= is +=)
        /// </summary>
        public string ErrorMessage
        {
            get { return ErrorMessage; }
            set
            {
                if(!ErrorMessage.Equals("") & ErrorMessage != null)
                {
                    ErrorMessage += "\n" + value;
                }
                else
                {
                    ErrorMessage = value;
                }
            }
        }
        public bool? Success { get; set; }

        public IResult()
        {
            ErrorMessage = "";
            Success = false;
        }
    }
}
