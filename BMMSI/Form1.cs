using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Controler;
using DTO;
using DTO.DTOEventArgs;
using FANN.Net;

namespace BMMSI
{
    /// <summary>
    /// Główny formularz programu.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            button5.Enabled = false;
        }

        /// <summary>
        /// Metoda obsługi zdażenia załadowania formularza. Uruchamia proces inicjalizacji.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Parametry zdarzenia.</param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            MainControler.Instance.InitializationProgressChanged += 
                (s, eArgs) =>
                {
                    try
                    {
                        progressBar1.BeginInvoke((Action)(() => progressBar1.Value = (int)(eArgs.Progress)));
                    }
                    catch
                    {
                    }
                };

            MainControler.Instance.InitializationStatusChanged +=
                (s, eArgs) =>
                {
                    try
                    {
                        labelInfo.BeginInvoke((Action)(() => labelInfo.Text = eArgs.Status));
                    }
                    catch
                    {
                    }
                };

            MainControler.Instance.InitializationComplete +=
                (s, eArgs) =>
                {
                    this.button1.BeginInvoke((Action)(() => this.button1.Enabled = true));
                    this.button5.BeginInvoke((Action)(() => this.button5.Enabled = true));
                };

            MainControler.Instance.MaxComputingThreads = 6;
            Task t = Task.Factory.StartNew((Action)MainControler.Instance.Initialize);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PredictionResult results;
            if (folderBrowserDialogNetwork.ShowDialog() == DialogResult.OK)
            {
                results = MainControler.Instance.PredictTrendDirection(this.dateTimePicker1.Value, folderBrowserDialogNetwork.SelectedPath);
                if (results == null)
                    MessageBox.Show("Error with selected network has occured. Please try to create this network again or check if correct folder was picked");
                else
                {
                    MessageBox.Show("Prediction for " + this.dateTimePicker1.Value.ToShortDateString() + "\n\n" + results.Trends[0].Direction.ToString() + " " +
                       results.Trends[0].Propability.ToString() + "\n" + results.Trends[1].Direction.ToString() + " " +
                       results.Trends[1].Propability.ToString() + "\n" + results.Trends[2].Direction.ToString() + " " +
                       results.Trends[2].Propability.ToString() + "\n\nUsed network parameters: \n\n" +
                       "Period number: " + results.PeriodNo.ToString() + "\n" + "Patterns number: " + results.PatternsNo.ToString() + "\n" +
                       "Hidden layer neuron count: " + results.NeuronNo.ToString() + "\n" + "Epochs number: " + results.EpochsNo.ToString() + "\n" +
                       "MSE: " + results.MSE.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form options = new Option();
            options.Show();
        }
    }
}
