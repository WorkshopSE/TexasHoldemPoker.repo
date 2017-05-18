﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Responses
{
    public abstract class IResponse
    {
        public string ErrorMessage { get; set; }
        public bool? Success { get; set; }
    }
}
