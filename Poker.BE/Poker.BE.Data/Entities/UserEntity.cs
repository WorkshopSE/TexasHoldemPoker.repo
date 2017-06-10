using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data.Entities
{
    public class UserEntity
    {
        // UNDONE: validate with Gal that User.ID is not needed.
        //[Key]
        //public int ID { get; set; }

        [Key]
        public string UserName { get; set; }

        public string Password { get; set; }
        public virtual MoneyStorageEntity Bank { get; set; }
        public virtual AvatarEntity Avatar { get; set; }
        public virtual StatisticsEntity Statistics { get; set; }
        public virtual ICollection<PlayerEntity> Players { get; set; }
    }
}
