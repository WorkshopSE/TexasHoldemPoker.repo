﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Poker.BE.Data.Entities
{
    public class PlayerEntity
    {
        public int ID { get; set; }
        public string NickName { get; set; }
        public string State { get; set; }
        public virtual MoneyStorageEntity Wallet { get; set; }
    }
}