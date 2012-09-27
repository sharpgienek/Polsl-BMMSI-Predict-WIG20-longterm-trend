using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    /// <summary>
    /// Klasa służaća do przechowywania wyników przewidywania oraz paramatrów sieci użytej do predykcji.
    /// </summary>
    public class PredictionResult
    {
        /// <summary>
        /// Lista faktycznych wyników - trendy wraz z ich prawdopodobieństwem.
        /// </summary>
        public List<TrendDirectionWithPropability> Trends { set; get; }
        /// <summary>
        /// Liczba okresów - parametr sieci.
        /// </summary>
        public int PeriodNo { set; get; }
        /// <summary>
        /// Liczba wzorców - parametr sieci.
        /// </summary>
        public int PatternsNo { set; get; }
        /// <summary>
        /// Liczba neuronów w warstwie ukrytej - parametr sieci.
        /// </summary>
        public int NeuronNo { set; get; }
        /// <summary>
        /// Liczba epok - parametr sieci.
        /// </summary>
        public int EpochsNo { set; get; }
        /// <summary>
        /// Bład średniokwadratowy sieci.
        /// </summary>
        public double MSE { set; get; }
    }
}
