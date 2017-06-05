using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility
{
    /// <summary>
    /// Comparator for shallow compare
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AddressComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            return x.GetHashCode() == y.GetHashCode();
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
