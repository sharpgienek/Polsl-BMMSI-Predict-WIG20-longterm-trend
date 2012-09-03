using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.DTOEventArgs
{
    public class ComputingThreadProgressChangedEventArgs : EventArgs
    {
        public int TaskNumber { get; set; }
        public uint CurrentEpoch { get; set; }
        public uint MaxEpochs { get; set; }
        public TimeSpan TimeLeft { get; set; }
        public TimeSpan ElaspedTime { get; set; }
        private double taskProgress;
        public double TaskProgress
        {
            get
            {
                return this.taskProgress;
            }
            set
            {
                if (value >= 100)
                {
                    this.taskProgress = 100;
                }
                else
                {
                    if (value <= 0)
                    {
                        this.taskProgress = 0;
                    }
                    else
                    {
                        this.taskProgress = value;
                    }
                }
            }
        }
    }
}
