using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
    public abstract class IResult
    {
        private string errorMessage = "";

        /// <summary>
        /// error message log with accumulate set (= is +=)
        /// </summary>
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if(!errorMessage.Equals("") & errorMessage != null)
                {
                    errorMessage += "\n" + value;
                }
                else
                {
                    errorMessage = value;
                }
            }
        }
        public bool? Success { get; set; }

        public IResult()
        {
            //errorMessage = "";
            Success = false;
        }
    }
}
