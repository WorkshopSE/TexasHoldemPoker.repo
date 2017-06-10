using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Poker.BE.Data.Entities
{
    public class Player
    {
        public int ID { get; set; }
        public string NickName { get; set; }
        public string State { get; set; }
        public MoneyStorage Wallet { get; set; }
    }
}
