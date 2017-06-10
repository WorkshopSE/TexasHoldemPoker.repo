using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data
{
    public interface IAccessible<TEntity> where TEntity : class
    {
        int Update(out TEntity entity);
    }
}
