using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data
{
    public abstract class IAccessible<TEntity> where TEntity : class
    {
        protected TEntity Entity = default(TEntity);

        public abstract int Update(out TEntity entity);
    }
}
