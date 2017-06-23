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

    }
}