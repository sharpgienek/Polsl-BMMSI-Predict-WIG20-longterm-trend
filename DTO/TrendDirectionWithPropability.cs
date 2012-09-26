using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    /// <summary>
    /// Klasa reprezentująca powiązanie pomiędzy danym trendem, a prawdopodobieństwem jego wystąpienia.
    /// </summary>
    public class TrendDirectionWithPropability
    {
        /// <summary>
        /// Kierunek trendu.
        /// </summary>
        public TrendDirection Direction { get; set; }

        /// <summary>
        /// Prawdopodobieństwo wystąpienia trendu.
        /// </summary>
        public double Propability { get; set; }
    }
}
