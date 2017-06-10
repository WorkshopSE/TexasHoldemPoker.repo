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
    class Access : IDisposable
    {
        private MainContext _context;

        public Access(MainContext context)
        {
            _context = context;
            // TODO: initiate repositories (= new...)
        }

        #region Repositories Exposure
        // TODO public repositoryclass name {get; private set;}
        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
