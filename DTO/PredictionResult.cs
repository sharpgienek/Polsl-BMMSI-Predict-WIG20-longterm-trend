using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class PredictionResult
    {
        public List<TrendDirectionWithPropability> Trends { set; get; }
        public int PeriodNo { set; get; }
        public int PatternsNo { set; get; }
        public int NeuronNo { set; get; }
        public int EpochsNo { set; get; }
        public double MSE { set; get; }
    }
}
