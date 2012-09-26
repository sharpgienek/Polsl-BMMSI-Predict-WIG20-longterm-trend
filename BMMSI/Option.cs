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
    /// <summary>
    /// Klasa reprezentująca formularz tworzenia nowych sieci.
    /// </summary>
    public partial class Option : Form
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public Option()
        {
            InitializeComponent();

            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            textBoxTrainigPath.Text = currentPath + "\\training data\\";
            textBoxTrainingMinPeriod.Text = (10).ToString();
            textBoxTrainingMaxPeriod.Text = (30).ToString();
            textBoxTrainingPeriodStep.Text = (2).ToString();
            textBoxMinPattern.Text = (10).ToString();
            textBoxMaxPattern.Text = (200).ToString();
            textBoxPatternStep.Text = (10).ToString();
            dateTimePickerTrain.Value = MainControler.Instance.GetNextExchangeQuotationDate(new DateTime(2009, 11, 19), 0);
            textBoxTrainDateStep.Text = (6).ToString();

            textBoxTestPath.Text = currentPath + "\\test data\\";
            textBoxTestMinPeriodNo.Text = (10).ToString();
            textBoxTestMaxPeriodNo.Text = (30).ToString();
            textBoxTestPeriodStep.Text = (2).ToString();
            textBoxDesiredPatNo.Text = (100).ToString();
            dateTimePickerTest.Value  = MainControler.Instance.GetNextExchangeQuotationDate(new DateTime(2010, 9, 3), 0);
            textBoxTestDateStep.Text = (13).ToString();

            textBoxNetPath.Text = "networks\\";
            textBoxMaxEpochs.Text = (10000).ToString();
            textBoxMinEpochs.Text = (10).ToString();
            textBoxEpochsStep.Text = (10).ToString();
            textBoxMinHidden.Text = (0.5).ToString();
            textBoxMaxHidden.Text = (1.2).ToString();
            textBoxHiddenStep.Text = (0.1).ToString();
            textBoxMSE.Text = (0.04).ToString();

            textBoxThreads.Text = (6).ToString();

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
            textBoxThreads.Validating += validateIntegerTextBox;
        }

        /// <summary>
        /// Metoda obsługi guzika Cancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Metoda sprawdzająca, czy dane wprowadzone do textBox'a są wartością liczbową typu int.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateIntegerTextBox(object sender, CancelEventArgs e)
        {
            TextBox text = (TextBox) sender;
            int result;
            if (!int.TryParse(text.Text, out result) || result <= 0)
            {
                e.Cancel = true;
                text.Text = "";
                MessageBox.Show("You need to enter an integer");
            }
        }

        /// <summary>
        /// Metoda sprawdzająca, czy dane wprowadzone do textBox'a są wartością liczbową typu double.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateDoubleTextBox(object sender, CancelEventArgs e)
        {
            TextBox text = (TextBox)sender;
            double result;
            if (!double.TryParse(text.Text, out result)|| result <= 0)
            {
                e.Cancel = true;
                text.Text = "";
                MessageBox.Show("You need to enter a floating point number");
            }
        }

        /// <summary>
        /// Metoda tworzenia obiektu opcji tworzenia sieci na podstawie danych wprowadzonych do formularza.
        /// </summary>
        /// <returns>Obiekt opcji tworzenia sieci.</returns>
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

        /// <summary>
        /// Metoda obsługi guzika zapisania.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            MainControler.Instance.MaxComputingThreads = int.Parse(textBoxThreads.Text);
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
