using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DTO
{
    /// <summary>
    /// Klasa reprezentująca opcje tworzenia sieci neuronowych.
    /// </summary>
    [Serializable]
    public class OptionData
    {
        /// <summary>
        /// Ścieżka do folderu zawierającego dane treningowe.
        /// </summary>
        public string TrainingPath { get; set; }

        /// <summary>
        /// Minimalna liczba okresów dla danych treningowych.
        /// </summary>
        public int TrainingMinNumberOfPeriods { get; set; }

        /// <summary>
        /// Maksymalna liczba okresów dla danych treningowych.
        /// </summary>
        public int TrainingMaxNumberOfPeriods { get; set; }

        /// <summary>
        /// Krok pomiędzy kolejnymi liczbami okresów dla danych treningowych.
        /// </summary>
        public int TrainingPeriodsStep { get; set; }

        /// <summary>
        /// Minimalna liczba wzorców treningowych dla danych treningowych.
        /// </summary>
        public int MinTrainingPatterns { get; set; }

        /// <summary>
        /// Maksymalna liczba wzorców treningowych dla danych treningowych.
        /// </summary>
        public int MaxTrainingPatterns { get; set; }

        /// <summary>
        /// Krok pomiędzy kolejnymi liczbami wzorców treningowych dla danych treningowych.
        /// </summary>
        public int TrainingPatternsStep { get; set; }

        /// <summary>
        /// Pierwsza kluczowa data dla danych treningowych.
        /// </summary>
        public DateTime TrainingPatternsSearchStartDate { get; set; }

        /// <summary>
        /// Krok pomiędzy kolejnymi kluczowymi datami dla danych treningowych.
        /// </summary>
        public int TrainingExchangeDaysStep { get; set; }

        /// <summary>
        /// Ścieżka do folderu danych testowych.
        /// </summary>
        public string TestPath { get; set; }

        /// <summary>
        /// Minimalna liczba okresów dla danych testowych.
        /// </summary>
        public int TestMinNumberOfPeriods { get; set; }

        /// <summary>
        /// Maksymalna liczba okresów dla danych testowych.
        /// </summary>
        public int TestMaxNumberOfPeriods { get; set; }

        /// <summary>
        /// Krok pomiędzy kolejnymi liczbami okresów dla danych testowych.
        /// </summary>
        public int TestPeriodsStep { get; set; }

        /// <summary>
        /// Oczekiwana liczba wzorców testowych.
        /// </summary>
        public int DesiredNumberOfPatterns { get; set; }

        /// <summary>
        /// Pierwsza kluczowa data dla danych testujących.
        /// </summary>
        public DateTime TestPatternsSearchStartDate { get; set; }

        /// <summary>
        /// Krok pomiędzy kolejnymi kluczowymi datami dla danych testujących.
        /// </summary>
        public int TestExchangeDaysStep { get; set; }

        /// <summary>
        /// Ścieżka do folderu sici neuronowych.
        /// </summary>
        public string NetPath { get; set; }

        /// <summary>
        /// Maksymalna liczba epok.
        /// </summary>
        public int MaxEpochs { get; set; }

        /// <summary>
        /// Minimalna liczba epok.
        /// </summary>
        public int MinEpochs { get; set; }

        /// <summary>
        /// Mnożnik dla kolejnych wartości epok.
        /// </summary>
        public double MaxEpochsMultiplierStep { get; set; }

        /// <summary>
        /// Minimalny mnożnik dla liczby neuronów warstwy ukrytej.
        /// </summary>
        public double MinHiddenLayersMultiplier { get; set; }

        /// <summary>
        /// Maksymalny mnożnik dla liczby neuronów warstwy ukrytej.
        /// </summary>
        public double MaxHiddenLayersMultiplier { get; set; }

        /// <summary>
        /// Krok pomiędzy kolejnymi wartościami mnożnika dla liczby neuronów warstwy ukrytej.
        /// </summary>
        public double HiddenLayersMultiplierStep { get; set; }

        /// <summary>
        /// Wartość błędu MSE, której osiągnięcie przerywa szukanie lepszych sieci.
        /// </summary>
        public double DesiredMSE { get; set; }

        public int NumberOfNetworksToCreate
        {
            get
            {
                if (MaxEpochsMultiplierStep <= 0.0f || TrainingPatternsStep <= 0 || TrainingPeriodsStep <= 0 || HiddenLayersMultiplierStep <= 0)
                    return 0;
                int number = 0;
                for (int i = TrainingMinNumberOfPeriods; i <= TrainingMaxNumberOfPeriods; i += TrainingPeriodsStep)
                {
                    for (int j = MinTrainingPatterns; j <= MaxTrainingPatterns; j += TrainingPatternsStep)
                    {
                        for (double h = MinEpochs; h <= MaxEpochs; h *= MaxEpochsMultiplierStep)
                        {
                            for (double l = MinHiddenLayersMultiplier; l <= MaxHiddenLayersMultiplier; l += HiddenLayersMultiplierStep)
                            {
                                number++;
                            }
                        }
                    }
                }
                return number;
            }
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public OptionData()
        {            
        }
    }
}
