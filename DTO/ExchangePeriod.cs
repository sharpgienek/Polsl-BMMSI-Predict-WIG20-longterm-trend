using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class ExchangePeriod
    {
        public double OpenRate { get; set; }
        public double CloseRate { get; set; }
        public double PercentageChange
        {
            get
            {
                return (this.CloseRate - this.OpenRate) / this.OpenRate;
            }
        }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public double PublicTrading { get; set; }
    }
}
