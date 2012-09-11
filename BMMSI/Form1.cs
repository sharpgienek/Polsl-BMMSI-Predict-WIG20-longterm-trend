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
        /// Aktualnie tworzy formularz szczegółów tworzenia sieci wielowątkowo, którego obsługa jest wyłączona bo powoduje błędy. todo.
        /// </summary>
        /// <param name="sender">asdf</param>
        /// <param name="e">asdf</param>
        private void button4_Click(object sender, EventArgs e)
        {
            ComputingThreadsProgressForm progressForm = new ComputingThreadsProgressForm(MainControler.Instance.MaxComputingThreads);
            EventHandler<ComputingThreadProgressChangedEventArgs> computingThreadProgressChanngedHandler = (EventHandler<ComputingThreadProgressChangedEventArgs>)
                    ((s, eArgs) =>
                    {
                        ProgressBar pb = (ProgressBar)progressForm.Controls["Computing Thread ProgressBar " + eArgs.TaskNumber.ToString()];
                        Label progressLabel = (Label)progressForm.Controls["Computing Thread Progress Label " + eArgs.TaskNumber.ToString()];
                        Label timeLabel = (Label)progressForm.Controls["Computing Thread Time Label " + eArgs.TaskNumber.ToString()];
                        try
                        {
                            pb.BeginInvoke(
                                    (Action)(() =>
                                    {
                                        pb.Value = (int)eArgs.TaskProgress;
                                    }
                                    )
                                );

                            progressLabel.BeginInvoke(
                                    (Action)
                                    (
                                        () =>
                                        {
                                            progressLabel.Text = ((int)eArgs.TaskProgress).ToString() + " % " + eArgs.CurrentEpoch.ToString() + " / " + eArgs.MaxEpochs.ToString();
                                        }
                                    )
                                );

                            timeLabel.BeginInvoke(
                                (Action)
                                (
                                    () =>
                                    {
                                        timeLabel.Text = "Elasped time: ";
                                        if (eArgs.ElaspedTime.TotalDays > 1)
                                        {
                                            timeLabel.Text += ((int)(eArgs.ElaspedTime.Days)).ToString() + "d ";
                                        }

                                        if (eArgs.ElaspedTime.TotalHours > 1)
                                        {
                                            timeLabel.Text += ((int)(eArgs.ElaspedTime.Hours)).ToString() + "h ";
                                        }

                                        if (eArgs.ElaspedTime.TotalMinutes > 1)
                                        {
                                            timeLabel.Text += ((int)(eArgs.ElaspedTime.Minutes)).ToString() + "m ";
                                        }

                                        timeLabel.Text += ((int)(eArgs.ElaspedTime.Seconds)).ToString() + "s    ";

                                        timeLabel.Text += "Time left: ";

                                        if (eArgs.TimeLeft.TotalDays > 1)
                                        {
                                            timeLabel.Text += ((int)(eArgs.TimeLeft.Days)).ToString() + "d ";
                                        }

                                        if (eArgs.TimeLeft.TotalHours > 1)
                                        {
                                            timeLabel.Text += ((int)(eArgs.TimeLeft.Hours)).ToString() + "h ";
                                        }

                                        if (eArgs.TimeLeft.TotalMinutes > 1)
                                        {
                                            timeLabel.Text += ((int)(eArgs.TimeLeft.Minutes)).ToString() + "m ";
                                        }

                                        timeLabel.Text += ((int)(eArgs.TimeLeft.Seconds)).ToString() + "s";
                                    }
                                )
                                );
                        }
                        catch
                        {
                        }
                    }
                    );

            progressForm.Shown += (EventHandler)(
                (shownSender, ShownE) =>
            {
                MainControler.Instance.ComputingThreadProgressChanged += computingThreadProgressChanngedHandler;
                    
            }
            );

            progressForm.FormClosed +=
                (FormClosedEventHandler)((closedSender, closedE) =>
                {
                    MainControler.Instance.ComputingThreadProgressChanged -= computingThreadProgressChanngedHandler;
                });

            progressForm.Show();
        }

        /// <summary>
        /// Metoda obsługi zdażenia załadowania formularza. Uruchamia proces inicjalizacji.
        /// </summary>
        /// <param name="sender">Obiekt wysyłający.</param>
        /// <param name="e">Parametry zdarzenia.</param>
        private void Form1_Shown(object sender, EventArgs e)//todo nazwa metody.
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

        /// <summary>
        /// Metoda obsługi guzika ... . Powoduje przewidywanie trendu od dnia zaznaczonego w dateTimePickerze todo poprawić nazewnictwo i komentarz.
        /// </summary>
        /// <param name="sender">asdf.</param>
        /// <param name="e">asdf.</param>
        private void button5_Click(object sender, EventArgs e)//todo nazwa metody.
        {
            List<TrendDirectionWithPropability> results = new List<TrendDirectionWithPropability>();
            if (folderBrowserDialogNetwork.ShowDialog() == DialogResult.OK)
            {
                results = MainControler.Instance.PredictTrendDirection(this.dateTimePicker1.Value, folderBrowserDialogNetwork.SelectedPath);
                MessageBox.Show("Prediction for " + this.dateTimePicker1.Value.ToShortDateString() + "\n" + results[0].Direction.ToString() + " " +
                   results[0].Propability.ToString() + "\n" + results[1].Direction.ToString() + " " +
                   results[1].Propability.ToString() + "\n" + results[2].Direction.ToString() + " " +
                   results[2].Propability.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form options = new Option();
            options.Show();
        }
    }
}
