using System;
using System.Collections.Generic;

namespace Poker.BE.Service.Modules.Caches
{
    public interface ICache
    {
        /// <summary>
        /// Loading all the data from lower layers to this cache.
        /// </summary>
        /// <returns>true case essential Refresh had done</returns>
        bool Refresh();

        /// <summary>
        /// Clear all resources
        /// </summary>
        void Clear();

        /// <summary>
        /// get from cache using Refresh() if this level of cache doesn't contain
        /// the requested object.
        /// </summary>
        /// <typeparam name="TKey">dictionary key type</typeparam>
        /// <typeparam name="TValue">dictionary value type</typeparam>
        /// <param name="dictionary">
        /// the dictionary at cache to search for the 
        /// requested object
        /// </param>
        /// <param name="key">key to search in dictionary</param>
        /// <param name="e">exception case object not found</param>
        /// <returns></returns>
        TValue RefreshAndGet<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, Exception e);

    }
}