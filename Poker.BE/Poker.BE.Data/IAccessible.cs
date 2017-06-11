using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data
{
    public interface IAccessible<TEntity> where TEntity : class
    {
        int CreateAccess();
        TEntity Entity { get; }

        /// <summary>
        /// Saving changes to the data base
        /// </summary>
        /// <returns>status code of context.SaveChanges()</returns>
        int Save();

        int Clear();

        /*Note: not needed at this moment*/
        //int UpdateAccess();
        //int UpdateEntity<T>(ref T entityField, T value);

    }
}
