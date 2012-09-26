using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    /// <summary>
    /// Klasa reprezentująca parametry pliku dla danych treningowych.
    /// </summary>
    public class TrainingDataFileParameters
    {
        /// <summary>
        /// Kluczowa data.
        /// </summary>
        public DateTime KeyDate { get; set; }

        /// <summary>
        /// Liczba okresów.
        /// </summary>
        public int NumberOfPeriods { get; set; }

        /// <summary>
        /// Liczba wzorców treningowych.
        /// </summary>
        public int NumberOfPatterns { get; set; }

        /// <summary>
        /// Nazwa pliku.
        /// </summary>
        public string FileName { get; set; }
    }
}
