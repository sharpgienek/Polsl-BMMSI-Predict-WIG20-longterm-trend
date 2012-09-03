using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.DTOEventArgs
{
    public class ProgressChangedEventArgs : EventArgs
    {
        public double Progress { get; set; }
    }
}
