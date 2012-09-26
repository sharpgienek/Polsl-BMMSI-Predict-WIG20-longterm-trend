using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    /// <summary>
    /// Klasa reprezentująca argumenty zdarzenia zmiany postępu inicjalizacji.
    /// </summary>
    public class UpdateInitProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Stan.
        /// </summary>
        public string Status { get; set; }

        private double progress;

        /// <summary>
        /// Procentowa wartość postępu.
        /// </summary>
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
