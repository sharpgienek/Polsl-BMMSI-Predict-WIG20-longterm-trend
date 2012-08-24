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
            double[] frequencies = new double[12];        
            frequencies[0] = (double)textBox1.Text.Count(c => c.Equals('a')) / textBox1.Text.Length;
            frequencies[1] = (double)textBox1.Text.Count(c => c.Equals('b')) / textBox1.Text.Length;
            frequencies[2] = (double)textBox1.Text.Count(c => c.Equals('c')) / textBox1.Text.Length;
            frequencies[3] = (double)textBox1.Text.Count(c => c.Equals('d')) / textBox1.Text.Length;
            frequencies[4] = (double)textBox1.Text.Count(c => c.Equals('e')) / textBox1.Text.Length;
            frequencies[5] = (double)textBox1.Text.Count(c => c.Equals('f')) / textBox1.Text.Length;
            frequencies[6] = (double)textBox1.Text.Count(c => c.Equals('g')) / textBox1.Text.Length;
            frequencies[7] = (double)textBox1.Text.Count(c => c.Equals('h')) / textBox1.Text.Length;
            frequencies[8] = (double)textBox1.Text.Count(c => c.Equals('i')) / textBox1.Text.Length;
            frequencies[9] = (double)textBox1.Text.Count(c => c.Equals('j')) / textBox1.Text.Length;
            frequencies[10] = (double)textBox1.Text.Count(c => c.Equals('k')) / textBox1.Text.Length;
            frequencies[11] = (double)textBox1.Text.Count(c => c.Equals(' ')) / textBox1.Text.Length;
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
                trainingValues.Add("12");
                trainingValues.Add("2");
            }

            foreach(double freq in frequencies)
            {
                trainingValues.Add(freq.ToString());
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
            layers.Add(12); // inputs
            layers.Add(3); // hidden
            layers.Add(2); // output

            net.CreateStandardArray(layers.ToArray());

            net.SetLearningRate((float)0.7);

            net.SetActivationSteepnessHidden(1.0);
            net.SetActivationSteepnessOutput(1.0);

            //  net.SetActivationFunctionHidden(ActivationFunction.SigmoidSymmetric);
            //  net.SetActivationFunctionOutput(ActivationFunction.SigmoidSymmetric);

            net.SetTrainStopFunction(StopFunction.Bit);
            net.SetBitFailLimit(0.01f);
            // Set additional properties such as the training algorithm
            //net.SetTrainingAlgorithm(FANN::TRAIN_QuickProp);

            TrainingData data = new TrainingData();
            data.ReadTrainFromFile("training.txt");

            net.InitWeights(data);

            net.TrainOnData(data,
                1000, // max iterations
                10, // iterations between report
                0 //desired error
                );

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
            MainControler.DownloadData((Action<double>)UpdateProgressBar);
        }

        private void UpdateProgressBar(double progress)
        {
            if ((int)(progress * 100) <= 100)
            {
                progressBar1.Value = (int)(progress * 100);
            }
            else
            {
                progressBar1.Value = 100;
            }
        }
    }
}
