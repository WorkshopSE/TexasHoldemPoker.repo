using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data.Entities
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Key]
        public string UserName { get; set; }

        public string Password { get; set; }
        public virtual MoneyStorage Bank { get; set; }
        public virtual Avatar Avatar { get; set; }
        public virtual Statistics Statistics { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
