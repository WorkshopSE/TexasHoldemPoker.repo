using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility
{
    public abstract class MoneyStorage
    {
        #region Constants
        public enum Currency
        {
            USD,
            NIS,
            CHIP
        }

        #endregion

        #region Fields
        /// <summary>
        /// caching the gate value that we got from the service responsible to retrieve the gate value.
        /// so don't need to call this service more than once (for a wallet). to improve performance.
        /// </summary>
        private Dictionary<Currency, double> moneyGateCache = default(Dictionary<Currency, double>);

        private Currency currency;
        private double basicAmount;
        #endregion

        #region Properties
        /// <summary>
        /// the evaluated value of the wallet - by currency exchange.
        /// </summary>
        public double Value
        {
            get
            {
                if (!moneyGateCache.ContainsKey(currency))
                {
                    moneyGateCache.Add(currency, RetrieveGate(currency));
                }

                return basicAmount * moneyGateCache[currency];
            }
            set
            {
                if (!moneyGateCache.ContainsKey(currency))
                {
                    moneyGateCache.Add(currency, RetrieveGate(currency));
                }
                basicAmount = value / moneyGateCache[currency];
            }
        }
        #endregion

        #region Constructors
        public MoneyStorage()
        {
            currency = Currency.USD;
            moneyGateCache = new Dictionary<Currency, double>();
            basicAmount = 0;
        }

        public MoneyStorage(Currency currency): this()
        {
            this.currency = currency;
        }

        public MoneyStorage(Currency currency, double amount): this(currency)
        {
            basicAmount = amount;
        }
        #endregion

        #region Private Functions
        private double RetrieveGate(Currency currency)
        {
            var result = default(double);

            switch (currency)
            {
                // UNDONE: call a service that gets the actual currency gate.
                case Currency.USD:
                case Currency.NIS:
                case Currency.CHIP:
                default:
                    result = 1.0;
                    break;
            }

            return result;
        }
        #endregion

        #region Methods

        #endregion
    }
}
