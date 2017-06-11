using Poker.BE.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Data
{
    /// <summary>
    /// Unit Of Work for Repository pattern, disposable.
    /// </summary>
    public class Access : IDisposable
    {
        private MainContext _context;

        public Access(MainContext context)
        {
            _context = context;

            // TODO: initiate repositories (= new...)
            UserRepository = new UserRepository(context);
        }

        #region Repositories Exposure
        // TODO public repositoryClass name {get; private set;}
        public UserRepository UserRepository { get; private set; }
        #endregion

        public int Save()
        {
            int result = 0;
            try
            {
                result = _context.SaveChanges();
            }
            catch (Exception)
            {
                // TODO: log this data-base exception
                result = -1;
            }

            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
