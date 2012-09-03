using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FANN.Net;
using Controler;
using System.Threading;
using System.Globalization;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reflection;
using DTO;
using DTO.DTOEventArgs;

namespace BMMSI
{
    public partial class Form1 : Form
    {
        private string trainingFilePath;
        private NeuralNet net;
        public Form1()
        {
            InitializeComponent();
            this.trainingFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\training.txt";
            if (File.Exists("language.net"))
            {
                this.net = new NeuralNet();
                this.net.CreateFromFile("language.net");
            }
        }

        private double[] GetFrequencies()
        {
            double[] frequencies = new double[33];        
            frequencies[0] = (double)textBox1.Text.Count(c => c.Equals('a')) / textBox1.Text.Length;
            frequencies[1] = (double)textBox1.Text.Count(c => c.Equals('ą')) / textBox1.Text.Length;
            frequencies[2] = (double)textBox1.Text.Count(c => c.Equals('b')) / textBox1.Text.Length;
            frequencies[3] = (double)textBox1.Text.Count(c => c.Equals('c')) / textBox1.Text.Length;
            frequencies[4] = (double)textBox1.Text.Count(c => c.Equals('ć')) / textBox1.Text.Length;
            frequencies[5] = (double)textBox1.Text.Count(c => c.Equals('d')) / textBox1.Text.Length;
            frequencies[6] = (double)textBox1.Text.Count(c => c.Equals('e')) / textBox1.Text.Length;
            frequencies[7] = (double)textBox1.Text.Count(c => c.Equals('ę')) / textBox1.Text.Length;
            frequencies[8] = (double)textBox1.Text.Count(c => c.Equals('f')) / textBox1.Text.Length;
            frequencies[9] = (double)textBox1.Text.Count(c => c.Equals('g')) / textBox1.Text.Length;
            frequencies[11] = (double)textBox1.Text.Count(c => c.Equals('h')) / textBox1.Text.Length;
            frequencies[12] = (double)textBox1.Text.Count(c => c.Equals('i')) / textBox1.Text.Length;
            frequencies[13] = (double)textBox1.Text.Count(c => c.Equals('j')) / textBox1.Text.Length;
            frequencies[14] = (double)textBox1.Text.Count(c => c.Equals('k')) / textBox1.Text.Length;
            frequencies[15] = (double)textBox1.Text.Count(c => c.Equals('l')) / textBox1.Text.Length;
            frequencies[16] = (double)textBox1.Text.Count(c => c.Equals('ł')) / textBox1.Text.Length;
            frequencies[17] = (double)textBox1.Text.Count(c => c.Equals('m')) / textBox1.Text.Length;
            frequencies[18] = (double)textBox1.Text.Count(c => c.Equals('n')) / textBox1.Text.Length;
            frequencies[19] = (double)textBox1.Text.Count(c => c.Equals('ń')) / textBox1.Text.Length;
            frequencies[20] = (double)textBox1.Text.Count(c => c.Equals('o')) / textBox1.Text.Length;
            frequencies[21] = (double)textBox1.Text.Count(c => c.Equals('ó')) / textBox1.Text.Length;
            frequencies[22] = (double)textBox1.Text.Count(c => c.Equals('p')) / textBox1.Text.Length;
            frequencies[23] = (double)textBox1.Text.Count(c => c.Equals('r')) / textBox1.Text.Length;
            frequencies[24] = (double)textBox1.Text.Count(c => c.Equals('s')) / textBox1.Text.Length;
            frequencies[25] = (double)textBox1.Text.Count(c => c.Equals('ś')) / textBox1.Text.Length;
            frequencies[26] = (double)textBox1.Text.Count(c => c.Equals('t')) / textBox1.Text.Length;
            frequencies[27] = (double)textBox1.Text.Count(c => c.Equals('u')) / textBox1.Text.Length;
            frequencies[28] = (double)textBox1.Text.Count(c => c.Equals('w')) / textBox1.Text.Length;
            frequencies[29] = (double)textBox1.Text.Count(c => c.Equals('y')) / textBox1.Text.Length;
            frequencies[30] = (double)textBox1.Text.Count(c => c.Equals('z')) / textBox1.Text.Length;
            frequencies[31] = (double)textBox1.Text.Count(c => c.Equals('ź')) / textBox1.Text.Length;
            frequencies[32] = (double)textBox1.Text.Count(c => c.Equals('ż')) / textBox1.Text.Length;
            return frequencies;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] frequencies = GetFrequencies(); 
                        
            List<string> trainingValues = new List<string>();
            if (File.Exists(trainingFilePath))
            {
                int numberOfPatterns;
                using (StreamReader trainingFileStreamReader = new StreamReader(trainingFilePath))
                {
                    numberOfPatterns = Convert.ToInt32(trainingFileStreamReader.ReadLine());
                }

                numberOfPatterns++;
                
                using (FileStream fs = new FileStream(trainingFilePath, FileMode.Open, FileAccess.Write))
                {
                    fs.Position = 0;
                    byte[] newTextBytes = Encoding.ASCII.GetBytes(numberOfPatterns.ToString());
                    fs.Write(newTextBytes, 0, newTextBytes.Length);
                }

                trainingValues.Add("");
            }
            else
            {
                trainingValues.Add("1");
                trainingValues.Add("33");
                trainingValues.Add("2");
            }

            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            foreach(double freq in frequencies)
            {
                trainingValues.Add(freq.ToString(nfi));
            }

             trainingValues.Add("");
            if (checkBox1.Checked)
            {
                 trainingValues.Add("1 0");
            }
            else
            {
                 trainingValues.Add("0 1");
            }
            File.AppendAllLines(trainingFilePath, trainingValues);
        }

        private void Learn()
        {
            NeuralNet net = new NeuralNet();
            List<uint> layers = new List<uint>();
            layers.Add(33); // inputs
            layers.Add(20); // hidden
            layers.Add(2); // output

            net.CreateStandardArray(layers.ToArray());

            net.SetLearningRate((float)0.7);

          //  net.SetActivationSteepnessHidden(1.0);
          //  net.SetActivationSteepnessOutput(1.0);

             net.SetActivationFunctionHidden(ActivationFunction.SigmoidSymmetric);
             net.SetActivationFunctionOutput(ActivationFunction.SigmoidSymmetric);

           // net.SetTrainStopFunction(StopFunction.Bit);
           // net.SetBitFailLimit(0.001f);

            // Set additional properties such as the training algorithm
            //net.SetTrainingAlgorithm(FANN::TRAIN_QuickProp);

            TrainingData data = new TrainingData();
            if(data.ReadTrainFromFile("training.txt"))
            {
            }
           // net.InitWeights(data);

            net.Callback += (nn, train, max_epochs, epochs_between_reports, de, epochs)
                    =>
                    {
                        File.AppendAllText(Path.GetDirectoryName(Application.ExecutablePath) + "\\training log.txt", "Epochs: " + epochs + " Current Error: " + nn.GetMSE() + "\n");
                        return 0;
                    };

           // net.TrainOnFile(, 1000, 10, 0);

            Stopwatch sw = new Stopwatch();
            sw.Start();

           net.TrainOnData(data,
                100000, // max iterations
                10, // iterations between report
                0 //desired error
                );
           sw.Stop();
           // net.te
            net.Save("language.net");
            this.net = net;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Learn();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double[] frequencies = GetFrequencies();
            double[] r = net.Run(frequencies);
        }

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
                    this.button4.BeginInvoke((Action)(() => this.button4.Enabled = true));
                };

            MainControler.Instance.MaxComputingThreads = 6;
            Task t = Task.Factory.StartNew((Action)MainControler.Instance.Initialize);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainControler.Instance.PredictTrendDirection(this.dateTimePicker1.Value);
        }
    }
}
