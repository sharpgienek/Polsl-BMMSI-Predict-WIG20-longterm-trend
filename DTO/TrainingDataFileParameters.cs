using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class TrainingDataFileParameters
    {
        public DateTime KeyDate { get; set; }
        public int NumberOfPeriods { get; set; }
        public int NumberOfPatterns { get; set; }
        public string FileName { get; set; }
    }
}
