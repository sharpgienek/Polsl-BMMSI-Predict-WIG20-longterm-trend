using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class UpdateInitProgressEventArgs : EventArgs
    {
        public string Status { get; set; }

        private double progress;

        public double Progress
        {
            get
            {
                return this.progress;
            }
            set
            {
                if (value >= 100)
                {
                    this.progress = 100;
                    return;
                }

                if (value <= 0)
                {
                    this.progress = 0;
                    return;
                }

                this.progress = value;
            }
        }
    }
}
