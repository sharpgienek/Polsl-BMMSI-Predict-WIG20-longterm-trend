using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DTO;
using Controler;

namespace BMMSI
{
    public partial class Option : Form
    {
        public Option()
        {
            InitializeComponent();

            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            textBoxTrainigPath.Text = currentPath + "\\training data\\";
            textBoxTrainingMinPeriod.Text = (6).ToString();
            textBoxTrainingMaxPeriod.Text = (20).ToString();
            textBoxTrainingPeriodStep.Text = (1).ToString();
            textBoxMinPattern.Text = (10).ToString();
            textBoxMaxPattern.Text = (200).ToString();
            textBoxPatternStep.Text = (10).ToString();
            dateTimePickerTrain.Value = MainControler.Instance.GetNextExchangeQuotationDate(new DateTime(2012, 8, 24), 0);
            textBoxTrainDateStep.Text = (2).ToString();

            textBoxTestPath.Text = currentPath + "\\test data\\";
            textBoxTestMinPeriodNo.Text = (6).ToString();
            textBoxTestMaxPeriodNo.Text = (20).ToString();
            textBoxTestPeriodStep.Text = (1).ToString();
            textBoxDesiredPatNo.Text = (100).ToString();
            dateTimePickerTest.Value  = MainControler.Instance.GetNextExchangeQuotationDate(new DateTime(2012, 8, 24), -1);
            textBoxTestDateStep.Text = (2).ToString();

            textBoxNetPath.Text = "networks\\";
            textBoxMaxEpochs.Text = (10000).ToString();
            textBoxMinEpochs.Text = (10).ToString();
            textBoxEpochsStep.Text = (10).ToString();
            textBoxMinHidden.Text = (0.2).ToString();
            textBoxMaxHidden.Text = (1.2).ToString();
            textBoxHiddenStep.Text = (0.05).ToString();
            textBoxMSE.Text = (0.052).ToString();

            textBoxDesiredPatNo.Validating += validateIntegerTextBox;
            textBoxEpochsStep.Validating += validateDoubleTextBox;
            textBoxHiddenStep.Validating += validateDoubleTextBox;
            textBoxMaxEpochs.Validating += validateIntegerTextBox;
            textBoxMaxHidden.Validating += validateDoubleTextBox;
            textBoxMaxPattern.Validating += validateIntegerTextBox;
            textBoxMinEpochs.Validating += validateIntegerTextBox;
            textBoxMinHidden.Validating += validateDoubleTextBox;
            textBoxMinPattern.Validating += validateIntegerTextBox;
            textBoxPatternStep.Validating += validateIntegerTextBox;
            textBoxTestDateStep.Validating += validateIntegerTextBox;
            textBoxTestMaxPeriodNo.Validating += validateIntegerTextBox;
            textBoxTestMinPeriodNo.Validating += validateIntegerTextBox;
            textBoxTestPeriodStep.Validating += validateIntegerTextBox;
            textBoxTrainDateStep.Validating += validateIntegerTextBox;
            textBoxTrainingMaxPeriod.Validating += validateIntegerTextBox;
            textBoxTrainingMinPeriod.Validating += validateIntegerTextBox;
            textBoxTrainingPeriodStep.Validating += validateIntegerTextBox;
            textBoxMSE.Validating += validateDoubleTextBox;
            //labelCreateNetwork.Text = ((int) 4 * (1.2 - 0.2) / 0.05).ToString();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void validateIntegerTextBox(object sender, CancelEventArgs e)
        {
            TextBox text = (TextBox) sender;
            int result;
            if (!int.TryParse(text.Text, out result))
            {
                e.Cancel = true;
                text.Text = "";
                MessageBox.Show("You need to enter an integer");
            }
        }

        private void validateDoubleTextBox(object sender, CancelEventArgs e)
        {
            TextBox text = (TextBox)sender;
            double result;
            if (!double.TryParse(text.Text, out result))
            {
                e.Cancel = true;
                text.Text = "";
                MessageBox.Show("You need to enter a floating point number");
            }
        }

        private OptionData CreateOptionDataFromForm()
        {
            OptionData option = new OptionData();
            try
            {
                option.TrainingPath = textBoxTrainigPath.Text;
                option.TrainingMinNumberOfPeriods = int.Parse(textBoxTrainingMinPeriod.Text);
                option.TrainingMaxNumberOfPeriods = int.Parse(textBoxTrainingMaxPeriod.Text);
                option.TrainingPeriodsStep = int.Parse(textBoxTrainingPeriodStep.Text);
                option.MinTrainingPatterns = int.Parse(textBoxMinPattern.Text);
                option.MaxTrainingPatterns = int.Parse(textBoxMaxPattern.Text);
                option.TrainingPatternsStep = int.Parse(textBoxPatternStep.Text);
                option.TrainingPatternsSearchStartDate = dateTimePickerTrain.Value;
                option.TrainingExchangeDaysStep = int.Parse(textBoxTrainDateStep.Text);

                option.TestPath = textBoxTestPath.Text;
                option.TestMinNumberOfPeriods = int.Parse(textBoxTestMinPeriodNo.Text);
                option.TestMaxNumberOfPeriods = int.Parse(textBoxTestMaxPeriodNo.Text);
                option.TestPeriodsStep = int.Parse(textBoxTestPeriodStep.Text);
                option.DesiredNumberOfPatterns = int.Parse(textBoxDesiredPatNo.Text);
                option.TestPatternsSearchStartDate = dateTimePickerTest.Value;
                option.TestExchangeDaysStep = int.Parse(textBoxTestDateStep.Text);

                option.NetPath = textBoxNetPath.Text;
                option.MaxEpochs = int.Parse(textBoxMaxEpochs.Text);
                option.MinEpochs = int.Parse(textBoxMinEpochs.Text);
                option.MaxEpochsMultiplierStep = double.Parse(textBoxEpochsStep.Text);
                option.MinHiddenLayersMultiplier = double.Parse(textBoxMinHidden.Text);
                option.MaxHiddenLayersMultiplier = double.Parse(textBoxMaxHidden.Text);
                option.HiddenLayersMultiplierStep = double.Parse(textBoxHiddenStep.Text);
                option.DesiredMSE = double.Parse(textBoxMSE.Text);
                return option;
            }
            catch
            {
                return null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            MainControler.Instance.CreateNewNeuralNets(CreateOptionDataFromForm());
            Close();
        }

        private void textBoxTrainingMinPeriod_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void Option_Shown(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void UpdateNumberOfNetworksToCreate()
        {
            OptionData options = CreateOptionDataFromForm();
            if (options != null)
            {
                this.labelCreateNetwork.Text = options.NumberOfNetworksToCreate.ToString();
            }
            else
            {
                this.labelCreateNetwork.Text = 0.ToString();
            }
        }

        private void textBoxTrainingMaxPeriod_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxTrainingPeriodStep_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxMinPattern_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxMaxPattern_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxPatternStep_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxMinEpochs_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxMaxEpochs_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxEpochsStep_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxMinHidden_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxMaxHidden_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }

        private void textBoxHiddenStep_TextChanged(object sender, EventArgs e)
        {
            UpdateNumberOfNetworksToCreate();
        }
    }
}
