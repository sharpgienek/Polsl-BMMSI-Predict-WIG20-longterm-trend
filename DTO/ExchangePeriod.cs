using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    /// <summary>
    /// Klasa reprezentująca okres notowań.
    /// </summary>
    public class ExchangePeriod
    {
        /// <summary>
        /// Wartość zamknięcia pierwszego dnia okresu.
        /// </summary>
        public double OpenRate { get; set; }

        /// <summary>
        /// Wartość zamknięcia ostatniego dnia okresu.
        /// </summary>
        public double CloseRate { get; set; }

        /// <summary>
        /// Procentowa zmiana.
        /// </summary>
        public double PercentageChange
        {
            get
            {
                return (this.CloseRate - this.OpenRate) / this.OpenRate;
            }
        }

        /// <summary>
        /// Data rozpoczęcia okresu.
        /// </summary>
        public DateTime PeriodStart { get; set; }

        /// <summary>
        /// Data końca okresu.
        /// </summary>
        public DateTime PeriodEnd { get; set; }

        /// <summary>
        /// Obroty.
        /// </summary>
        public double PublicTrading { get; set; }
    }
}
