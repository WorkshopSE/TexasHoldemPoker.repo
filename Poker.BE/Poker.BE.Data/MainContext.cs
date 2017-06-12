using Poker.BE.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data
{
    /// <summary>
    /// DBContext for data entities for entity framework
    /// </summary>
    public class MainContext : DbContext
    {
        public virtual DbSet<PlayerEntity> Players { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

        // TODO
        /*
         * DbSet<..> for all entities
         * */
    }
}
