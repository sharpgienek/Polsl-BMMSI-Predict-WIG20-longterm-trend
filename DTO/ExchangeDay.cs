using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    [Serializable]
    public class ExchangeDay
    {
        public Double CloseRate { get; set; }
        public Double PublicTrading { get; set; }
        public DateTime Date { get; set; }
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
