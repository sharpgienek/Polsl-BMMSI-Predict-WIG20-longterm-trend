using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class NeuralNetworkParameters
    {
        public TrainingDataFileParameters fileParameters { get; set; }
        public uint maxEpochs { get; set; }
        public double hiddenLayersMultiplier { get; set; }
        public string FileName
        {
            get
            {
                return this.fileParameters.FileName.Remove(fileParameters.FileName.Length - 4)
                        + " "
                        + ((uint)(((uint)(fileParameters.NumberOfPatterns * 2) - 1) * hiddenLayersMultiplier)).ToString()
                        + " "
                        + maxEpochs.ToString()
                        + ".net";
            }
        }
    }
}
