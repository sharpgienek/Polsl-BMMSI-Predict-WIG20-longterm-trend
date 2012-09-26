using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    /// <summary>
    /// Klasa reprezentująca powiązanie pomiędzy plikiem sieci a jego błędem MSE.
    /// </summary>
    public class NetworkMSE
    {
        /// <summary>
        /// Nazwa pliku sieci.
        /// </summary>
        public string NetworkFileName { get; set; }

        /// <summary>
        /// Błąd MSE.
        /// </summary>
        public double MSE { get; set; }
    }
}
