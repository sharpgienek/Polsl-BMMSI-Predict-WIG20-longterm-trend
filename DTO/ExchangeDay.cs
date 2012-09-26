using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    /// <summary>
    /// Klasa reprezentująca dzień notowań.
    /// </summary>
    [Serializable]
    public class ExchangeDay
    {
        /// <summary>
        /// Wartość zamknięcia.
        /// </summary>
        public Double CloseRate { get; set; }

        /// <summary>
        /// Obroty.
        /// </summary>
        public Double PublicTrading { get; set; }

        /// <summary>
        /// Data.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Operator rzutowania bezpośredniego na typ ExchangePeriod.
        /// </summary>
        /// <param name="day">Data.</param>
        /// <returns>Obiekt typu ExchangePeriod.</returns>
        static public implicit operator ExchangePeriod(ExchangeDay day)
        {
            if (day != null)
            {
                return new ExchangePeriod()
                {
                    OpenRate = day.CloseRate,
                    CloseRate = day.CloseRate,
                    PeriodStart = day.Date,
                    PeriodEnd = day.Date,
                    PublicTrading = day.PublicTrading,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
