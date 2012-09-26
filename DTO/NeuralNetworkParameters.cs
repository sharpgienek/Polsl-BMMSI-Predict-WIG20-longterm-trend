using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    /// <summary>
    /// Klasa reprezentująca parametry sieci neuronowej.
    /// </summary>
    public class NeuralNetworkParameters
    {
        /// <summary>
        /// Parametry zapisane w nazwie pliku treningowego będącego podstawą stworzenia sieci.
        /// </summary>
        public TrainingDataFileParameters fileParameters { get; set; }

        /// <summary>
        /// Maksymalna liczba epok.
        /// </summary>
        public uint maxEpochs { get; set; }

        /// <summary>
        /// Liczba przez którą należy pomnożyć liczbę neuronów wejściowych aby otrzymać liczbę neuronów warstwy ukrytej.
        /// </summary>
        public double hiddenLayersMultiplier { get; set; }

        /// <summary>
        /// Nazwa pliku do jakiego powinna zostać zapisana sieć.
        /// </summary>
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
