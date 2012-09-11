using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DTO
{
    [Serializable]
    public class OptionData
    {
        public string TrainingPath { get; set; }
        public int TrainingMinNumberOfPeriods { get; set; }
        public int TrainingMaxNumberOfPeriods { get; set; }
        public int TrainingPeriodsStep { get; set; }
        public int MinTrainingPatterns { get; set; }
        public int MaxTrainingPatterns { get; set; }
        public int TrainingPatternsStep { get; set; }
        public DateTime TrainingPatternsSearchStartDate { get; set; }
        public int TrainingExchangeDaysStep { get; set; }

        public string TestPath { get; set; }
        public int TestMinNumberOfPeriods { get; set; }
        public int TestMaxNumberOfPeriods { get; set; }
        public int TestPeriodsStep { get; set; }
        public int DesiredNumberOfPatterns { get; set; }
        public DateTime TestPatternsSearchStartDate { get; set; }
        public int TestExchangeDaysStep { get; set; }

        public string NetPath { get; set; }
        public int MaxEpochs { get; set; }
        public int MinEpochs { get; set; }
        public double MaxEpochsMultiplierStep { get; set; }
        public double MinHiddenLayersMultiplier { get; set; }
        public double MaxHiddenLayersMultiplier { get; set; }
        public double HiddenLayersMultiplierStep { get; set; }
        public double DesiredMSE { get; set; }

        public int NumberOfNetworksToCreate
        {
            get
            {
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

        public OptionData()
        {            
        }
    }
}
