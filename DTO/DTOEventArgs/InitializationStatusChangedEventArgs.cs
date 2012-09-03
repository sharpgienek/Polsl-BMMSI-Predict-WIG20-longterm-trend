using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.DTOEventArgs
{
    public class InitializationStatusChangedEventArgs : EventArgs
    {
        public string Status { get; set; }
    }
}
