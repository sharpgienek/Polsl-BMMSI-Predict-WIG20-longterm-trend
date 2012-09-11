using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DTO;
using DTO.DTOEventArgs;
using FANN.Net;
using Model;

namespace Controler
{
    /// <summary>
    /// Główny kontroler. Jest Singletonem.
    /// </summary>
    public class MainControler
    {
        private static MainControler instance;

        /// <summary>
        /// Zdarzenie zakończenia metody inicjalizacji.
        /// </summary>
        public event EventHandler InitializationComplete;

        /// <summary>
        /// Instancja kontrolera.
        /// </summary>
        public static MainControler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainControler();
                }

                return instance;
            }
        }

        /// <summary>
        /// Prywatny konstruktor zgodnie z modelem singletona.
        /// </summary>
        private MainControler()
        {
            MaxComputingThreads = 1;
        }

        /// <summary>
        /// Właściwość określająca maksymalną liczbę wątków obliczeniowych.
        /// </summary>
        public int MaxComputingThreads { get; set; }

        /// <summary>
        /// Zdarzenie zmiany statusu inicjalizacji.
        /// </summary>
        public event EventHandler<InitializationStatusChangedEventArgs> InitializationStatusChanged;

        private string initializationStatusAssignOnlyThruPropertySetter;

        /// <summary>
        /// Status inicjalizacji.
        /// </summary>
        public string InitializationStatus
        {
            get
            {
                return this.initializationStatusAssignOnlyThruPropertySetter;
            }
            private set
            {
                string oldValue = this.initializationStatusAssignOnlyThruPropertySetter;
                this.initializationStatusAssignOnlyThruPropertySetter = value;
                if (oldValue == null || !oldValue.Equals(this.initializationStatusAssignOnlyThruPropertySetter))
                {
                    EventHandler<InitializationStatusChangedEventArgs> temp = this.InitializationStatusChanged;
                    if (temp != null)
                    {
                        temp.Invoke(this, new InitializationStatusChangedEventArgs()
                        {
                            Status = this.initializationStatusAssignOnlyThruPropertySetter
                        });
                    }
                }
            }
        }

        /// <summary>
        /// OBSOLETE! todo.
        /// Zdarzenie zmiany postępu któregoś z wątków obliczeniowych. 
        /// </summary>
        public event EventHandler<ComputingThreadProgressChangedEventArgs> ComputingThreadProgressChanged;

        /// <summary>
        /// Zdarzenie zmiany postępu inicjalizacji.
        /// </summary>
        public event EventHandler<ProgressChangedEventArgs> InitializationProgressChanged;

        private double initializationProgressAssignOnlyThruPropertySetter;

        /// <summary>
        /// Właściwość określająca postęp metody inicjalizacji.
        /// </summary>
        public double InitializationProgress
        {
            get
            {
                return this.initializationProgressAssignOnlyThruPropertySetter;
            }

            private set
            {
                double oldInitializationProgress = this.initializationProgressAssignOnlyThruPropertySetter;

                if (value >= 100)
                {
                    this.initializationProgressAssignOnlyThruPropertySetter = 100;
                }
                else
                {
                    if (value <= 0)
                    {
                        this.initializationProgressAssignOnlyThruPropertySetter = 0;
                    }
                    else
                    {
                        this.initializationProgressAssignOnlyThruPropertySetter = value;
                    }
                }

                if (oldInitializationProgress != this.initializationProgressAssignOnlyThruPropertySetter)
                {
                    EventHandler<ProgressChangedEventArgs> temp = this.InitializationProgressChanged;
                    if (temp != null)
                    {
                        temp.Invoke(this, new ProgressChangedEventArgs()
                        {
                            Progress = this.initializationProgressAssignOnlyThruPropertySetter
                        });
                    }

                    if (this.initializationProgressAssignOnlyThruPropertySetter == 100)
                    {
                        EventHandler initializationCompleteEvent = this.InitializationComplete;
                        if (initializationCompleteEvent != null)
                        {
                            initializationCompleteEvent.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Wartość stała określająca datę pierwszego dnia działania GPW w Polsce.
        /// </summary>
        private readonly DateTime firstExchangeQuotationDate = new DateTime(1990, 4, 16);

        /// <summary>
        /// Metoda inicjalizacji.
        /// </summary>

        public void Initialize()
        {
            this.InitializationProgress = 0;
            int numberOfSteps = 1;
            int currentStep = 1;
            this.InitializationStatus = "Step " + currentStep.ToString() + "/" + numberOfSteps.ToString() + " : Downloading historical data";
            DataDownloader.Instance.DownloadProgressChanged +=
                (EventHandler<DownloadProgressChangedDTOEventArgs>)((sender, e) =>
                {
                    this.InitializationProgress = e.Progress / numberOfSteps;
                });

            Thread downloadThread = new Thread(new ThreadStart(DataDownloader.Instance.DownloadData));
            downloadThread.Start();
        }

        public void CreateNewNeuralNets(OptionData option)
        {
            this.InitializationProgress = 0;
            int numberOfSteps = 3;
            int currentStep = 1;
            //this.InitializationProgress = (double)currentStep * 100 / numberOfSteps;
            //currentStep++;
            Task t = new Task((Action)(
                    () =>
                    {
                        this.InitializationStatus = "Step " + currentStep.ToString() + "/" + numberOfSteps.ToString() + " : Generating training Data";

                        GenerateTrainingData(
                            option.TrainingPath,
                            option.TrainingMinNumberOfPeriods,
                            option.TrainingMaxNumberOfPeriods,
                            option.TrainingPeriodsStep,
                            option.MinTrainingPatterns,
                            option.MaxTrainingPatterns,
                            option.TrainingPatternsStep,
                            option.TrainingPatternsSearchStartDate,
                            option.TrainingExchangeDaysStep
                            );

                        this.InitializationProgress = (double)currentStep * 100 / numberOfSteps;
                        currentStep++;
                        this.InitializationStatus = "Step " + currentStep.ToString() + "/" + numberOfSteps.ToString() + " : Generating test Data";

                        GenerateTestData(
                            option.TestPath,
                            option.TestMinNumberOfPeriods,
                            option.TestMaxNumberOfPeriods,
                            option.TestPeriodsStep,
                            option.DesiredNumberOfPatterns,
                            option.TestPatternsSearchStartDate,
                            option.TestExchangeDaysStep
                            );

                        this.InitializationProgress = (double)currentStep * 100 / numberOfSteps;
                        currentStep++;
                        this.InitializationStatus = "Step " + currentStep.ToString() + "/" + numberOfSteps.ToString() + " : Creating and training networks";

                        CreateNetworks(
                            option.NetPath,
                            Path.GetDirectoryName(option.TrainingPath),
                            Path.GetDirectoryName(option.TestPath),
                            option.MaxEpochs,
                            option.MinEpochs,
                            option.MaxEpochsMultiplierStep,
                            option.MinHiddenLayersMultiplier,
                            option.MaxHiddenLayersMultiplier,
                            option.HiddenLayersMultiplierStep,
                            100 / (double)numberOfSteps,
                            option.DesiredMSE);
                    }
                    ), TaskCreationOptions.LongRunning
                    );
            t.Start();
        }

        /// <summary>
        /// Metoda tworzenia i uczenia sieci neuronowych.
        /// </summary>
        /// <param name="maxMaxEpochs">Maksymalna liczba epok.</param>
        /// <param name="minMaxEpochs">Minimalna liczba epok.</param>
        /// <param name="maxEpochsMultiplierStep">Krok mnożący liczby epok dla kolejnego rodzaju sieci. </param>
        /// <param name="minHiddenLayersMultiplier">Minimalny mnożnik dla warstw ukrytych.</param>
        /// <param name="maxHiddenLayersMultiplier">Maksymalny mnożnik dla wartw ukrytych.</param>
        /// <param name="hiddenLayersMultiplierStep">Krok dodający dla mnożnika warstw ukrytych.</param>
        /// <param name="methodProgressPart">Wartość (0 - 100) określająca jaka część postępu inicjalizacji należy do tej metody.</param>
        private void CreateNetworks(
            string path,
            string trainDataFolder,
            string testDataFolder,
            int maxMaxEpochs,
            int minMaxEpochs,
            double maxEpochsMultiplierStep,
            double minHiddenLayersMultiplier,
            double maxHiddenLayersMultiplier,
            double hiddenLayersMultiplierStep,
            double methodProgressPart,
            double desiredMSE
            )
        {
            double methodStartProgress = this.InitializationProgress;
            List<TrainingDataFileParameters> fileList = DataProvider.Instance.GetTrainingDataFilesParameters(trainDataFolder);
            List<NeuralNetworkParameters> parameters = new List<NeuralNetworkParameters>();
            foreach (TrainingDataFileParameters file in fileList)
            {
                for (double i = minMaxEpochs; i <= maxMaxEpochs; i *= maxEpochsMultiplierStep)
                {
                    for (double j = minHiddenLayersMultiplier; j <= maxHiddenLayersMultiplier; j += hiddenLayersMultiplierStep)
                    {
                        parameters.Add(new NeuralNetworkParameters()
                        {
                            fileParameters = file,
                            hiddenLayersMultiplier = j,
                            maxEpochs = (uint)i
                        });
                    }
                }
            }

            int numberOfNetworksToCreate = parameters.Count;
            int numberOfCreatedNetworks = 0;
            NetworkMSE lowestNetworkMSE = null;

            try
            {
                StringReader reader = new StringReader(File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + path + "LowestMSENetwork.xml"));
                XmlSerializer serializer = new XmlSerializer(typeof(NetworkMSE));
                lowestNetworkMSE = (NetworkMSE)serializer.Deserialize(reader);
            }
            catch
            {
                lowestNetworkMSE = null;
            }

            //small hack
            if (lowestNetworkMSE != null && lowestNetworkMSE.MSE < desiredMSE)
            {
                this.InitializationProgress = 100;
                return;
            }
            string[] testFiles = Directory.GetFiles(testDataFolder);
            List<TrainingData> testDataList = new List<TrainingData>();
            foreach (string testFile in testFiles)
            {
                TrainingData td = new TrainingData();
                if (td.ReadTrainFromFile(testDataFolder + "\\" + Path.GetFileName(testFile)))
                {
                    testDataList.Add(td);
                }
                else
                {//todo
                }
            }

            List<TrainingData> trainingDataList = new List<TrainingData>();
            foreach (TrainingDataFileParameters file in fileList)
            {
                TrainingData td = new TrainingData();
                if (td.ReadTrainFromFile(trainDataFolder + "\\" + file.FileName))
                {
                    trainingDataList.Add(td);
                }
                else
                {//todo
                }
            }

            string initStatus = this.InitializationStatus;
            Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + path);
            List<Task> taskList = new List<Task>();
            NeuralNet.CallbackType[] callbacksArray = new NeuralNet.CallbackType[this.MaxComputingThreads];
            //   Semaphore parameterListSemaphore = new Semaphore(1, 1);
            //    Semaphore InitializationProgressSemaphore = new Semaphore(1, 1);
            //   Semaphore lowestNetworkMSESemaphore = new Semaphore(1, 1);
            //   Semaphore testDataSemaphore = new Semaphore(1, 1);
            //   Semaphore trainAndTestDataInitializationSemaphore = new Semaphore(1, 1);
            Semaphore threadProgressSemaphore = new Semaphore(1, 1);
            //   Semaphore newNeuralNetworkSemaphore = new Semaphore(1, 1);
            //   Semaphore trainDataSemaphore = new Semaphore(1, 1);
            // Semaphore trainStartSemaphore = new Semaphore(1, 1);
            //  List<NeuralNet.CallbackType> c

            Semaphore allSem = new Semaphore(1, 1);
            TrainingData[] threadDataVars = new TrainingData[this.MaxComputingThreads];
            Stopwatch[] threadStopwatches = new Stopwatch[this.MaxComputingThreads];
            for (int i = 1; i <= this.MaxComputingThreads; i++)
            {
                int taskNumber = i;
                // TrainingData data = 
                threadDataVars[i - 1] = new TrainingData();
                threadStopwatches[i - 1] = new Stopwatch();
                NeuralNet.CallbackType callback =

                    (nn, train, max_epochs, epochs_between_reports, de, epochs)
                                            =>
                    {
                        //    trainStartSemaphore.Release();
                        threadProgressSemaphore.WaitOne();
                        EventHandler<ComputingThreadProgressChangedEventArgs> threadProgressChangedEvent = this.ComputingThreadProgressChanged;
                        if (threadProgressChangedEvent != null)
                        {
                            threadProgressChangedEvent.Invoke(this, new ComputingThreadProgressChangedEventArgs()
                            {
                                TaskNumber = taskNumber,
                                TaskProgress = epochs * 100 / max_epochs,
                                CurrentEpoch = epochs,
                                MaxEpochs = max_epochs,
                                //  TimeLeft = new TimeSpan(((sw.Elapsed.Ticks * max_epochs) / epochs) - sw.Elapsed.Ticks),
                                //  ElaspedTime = new TimeSpan(sw.Elapsed.Ticks)
                                // TimeLeft = new TimeSpan(((threadStopwatches[taskNumber - 1].Elapsed.Ticks * max_epochs) / epochs) - sw.Elapsed.Ticks),
                                TimeLeft = new TimeSpan(((threadStopwatches[taskNumber - 1].Elapsed.Ticks * max_epochs) / epochs) - threadStopwatches[taskNumber - 1].Elapsed.Ticks),
                                ElaspedTime = new TimeSpan(threadStopwatches[taskNumber - 1].Elapsed.Ticks)
                            });
                        }

                        threadProgressSemaphore.Release();
                        return 0;
                    };

                callbacksArray[i - 1] = ((NeuralNet.CallbackType)callback.Clone());

                Task t = new Task((Action)(
                    () =>
                    {
                        while (true)
                        {
                            allSem.WaitOne();
                            //  parameterListSemaphore.WaitOne();
                            if (parameters.Count == 0)
                            {
                                //  parameterListSemaphore.Release();
                                this.InitializationStatus = initStatus + " " + numberOfNetworksToCreate.ToString() + " / " + numberOfNetworksToCreate.ToString();
                                this.InitializationProgress = 100;
                                break;
                            }
                            else
                            {
                                NeuralNetworkParameters usedParameters = parameters.First();
                                parameters.RemoveAt(0);
                                //  parameterListSemaphore.Release();
                                //NeuralNet net = CreateNetwork(usedParameters);
                                if (!File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + path + usedParameters.FileName))
                                {
                                    //   newNeuralNetworkSemaphore.WaitOne();
                                    NeuralNet net = new NeuralNet();
                                    //   newNeuralNetworkSemaphore.Release();
                                    List<uint> layers = new List<uint>();
                                    layers.Add((uint)((usedParameters.fileParameters.NumberOfPeriods * 2) - 1)); // inputs
                                    layers.Add((uint)(layers[0] * usedParameters.hiddenLayersMultiplier)); // hidden
                                    layers.Add(3); // output
                                    net.CreateStandardArray(layers.ToArray());
                                    net.SetLearningRate((float)0.7);
                                    net.SetActivationFunctionHidden(ActivationFunction.SigmoidSymmetric);
                                    net.SetActivationFunctionOutput(ActivationFunction.SigmoidSymmetric);
                                    //  trainAndTestDataInitializationSemaphore.WaitOne();
                                    //TrainingData data = new TrainingData();
                                    //  trainAndTestDataInitializationSemaphore.Release();
                                    /*  while (!data.ReadTrainFromFile("training data\\" + usedParameters.fileParameters.FileName))
                                      {
                                      }*/
                                    // Stopwatch sw = new Stopwatch();


                                    //   sw.Reset();
                                    threadStopwatches[taskNumber - 1].Reset();


                                    net.Callback += callbacksArray[taskNumber - 1];
                                    /*   net.Callback += (nn, train, max_epochs, epochs_between_reports, de, epochs)
                                               =>
                                       {
                                           threadProgressSemaphore.WaitOne();
                                           EventHandler<ComputingThreadProgressChangedEventArgs> threadProgressChangedEvent = this.ComputingThreadProgressChanged;
                                           if (threadProgressChangedEvent != null)
                                           {
                                               threadProgressChangedEvent.Invoke(this, new ComputingThreadProgressChangedEventArgs()
                                               {
                                                   TaskNumber = taskNumber,
                                                   TaskProgress = epochs * 100 / max_epochs,
                                                   CurrentEpoch = epochs,
                                                   MaxEpochs = max_epochs,
                                                   TimeLeft = new TimeSpan(((sw.Elapsed.Ticks * max_epochs) / epochs) - sw.Elapsed.Ticks),
                                                   ElaspedTime = new TimeSpan(sw.Elapsed.Ticks)
                                               });
                                           }

                                           threadProgressSemaphore.Release();
                                           return 0;
                                       };*/

                                    //sw.Start();
                                    threadStopwatches[taskNumber - 1].Start();
                                    //   trainDataSemaphore.WaitOne();
                                    threadDataVars[taskNumber - 1] = trainingDataList.Find((e) => ((e.NumInputTrainData == layers[0]) && (e.Input.Length == usedParameters.fileParameters.NumberOfPatterns)));
                                    //  trainDataSemaphore.Release();
                                    //          trainStartSemaphore.WaitOne();
                                    allSem.Release();
                                    net.TrainOnData(threadDataVars[taskNumber - 1],
                                            usedParameters.maxEpochs, // max iterations
                                        //   usedParameters.maxEpochs / 100, // iterations between report
                                            0,
                                            0 //desired error
                                            );
                                    allSem.WaitOne();
                                    //  sw.Stop();
                                    threadStopwatches[taskNumber - 1].Stop();

                                    //   testDataSemaphore.WaitOne();
                                    //   trainAndTestDataInitializationSemaphore.WaitOne();
                                    /*  while (!data.ReadTrainFromFile("test data\\" + usedParameters.fileParameters.NumberOfPeriods + " " + usedParameters.fileParameters.KeyDate.AddDays(-1).ToShortDateString() + ".txt."))
                                      {//todo usunąć.
                                      }*/
                                    net.TestData(testDataList.Find((e) => e.NumInputTrainData == layers[0]));

                                    // trainAndTestDataInitializationSemaphore.Release();
                                    //   testDataSemaphore.Release();
                                    double mse = net.GetMSE();
                                    // lowestNetworkMSESemaphore.WaitOne();
                                    if (lowestNetworkMSE == null || lowestNetworkMSE.MSE > mse)
                                    {
                                        lowestNetworkMSE = new NetworkMSE()
                                        {
                                            MSE = mse,
                                            NetworkFileName = usedParameters.FileName
                                        };

                                        StringWriter writer = new StringWriter();

                                        XmlSerializer serializer = new XmlSerializer(typeof(NetworkMSE));
                                        serializer.Serialize(writer, lowestNetworkMSE);
                                        File.WriteAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + path + "LowestMSENetwork.xml", writer.ToString());
                                        //     lowestNetworkMSESemaphore.Release();
                                    }
                                    else
                                    {
                                        //    lowestNetworkMSESemaphore.Release();
                                    }

                                    net.Save(path + usedParameters.FileName);
                                }

                                //  InitializationProgressSemaphore.WaitOne();
                                this.InitializationStatus = initStatus + " " + numberOfCreatedNetworks.ToString() + " / " + numberOfNetworksToCreate.ToString();
                                numberOfCreatedNetworks++;
                                this.InitializationProgress = (numberOfCreatedNetworks * methodProgressPart / numberOfNetworksToCreate) + methodStartProgress;
                                //   InitializationProgressSemaphore.Release();

                            }
                            allSem.Release();
                        }
                    }
                    ), TaskCreationOptions.LongRunning
                    );
                taskList.Add(t);
            }

            foreach (Task t in taskList)
            {
                t.Start();
            }
        }

        private void GenerateTestData(
            string dataPath,
            int minNumberOfPeriods,
            int maxNumberOfPeriods,
            int periodsStep,
            int desiredNumberOfPatterns,
            DateTime patternsSearchStartDate,
            int exchangeDaysStep
            )
        {
            patternsSearchStartDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternsSearchStartDate, 0);
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            Directory.CreateDirectory(dataPath);
            for (int i = minNumberOfPeriods; i <= maxNumberOfPeriods; i += periodsStep)
            {
                List<string> trainingFileValues = new List<string>();
                trainingFileValues.Add("");
                trainingFileValues.Add((2 * i - 1).ToString());
                trainingFileValues.Add("3");
                trainingFileValues.Add("");

                int addedPatterns = 0;
                DateTime patternDate = patternsSearchStartDate;
                if (!File.Exists(dataPath + i.ToString() + " " + patternsSearchStartDate.ToShortDateString() + ".txt"))
                {
                    while ((addedPatterns < desiredNumberOfPatterns) && (patternDate > firstExchangeQuotationDate))
                    {
                        List<ExchangePeriod> historicalData = DataProvider.Instance.GetExchangePeriodsMergedByMovementDirectoryFromEndDate(i, patternDate, firstExchangeQuotationDate, 7);
                        if (historicalData.Count != i)
                        {
                            patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternDate, -exchangeDaysStep);
                            continue;
                        }

                        List<ExchangePeriod> resultData = DataProvider.Instance.GetExchangePeriodsMergedByMovementDirectoryFromStartDate(3, DateTime.Today, patternDate, 7);
                        if (resultData.Count != 3)
                        {
                            patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternDate, -exchangeDaysStep);
                            continue;
                        }

                        for (int h = 0; h < historicalData.Count; ++h)
                        {
                            trainingFileValues.Add(historicalData[h].PercentageChange.ToString(nfi));
                            if (h + 1 < historicalData.Count)
                            {
                                trainingFileValues.Add(((historicalData[h + 1].PublicTrading - historicalData[h].PublicTrading) / historicalData[h].PublicTrading).ToString(nfi));
                            }
                        }

                        trainingFileValues.Add("");

                        TrendDirection direction = GetTrendDirection(resultData, 0);
                        switch (direction)
                        {
                            case TrendDirection.Down:
                                trainingFileValues.Add("1 0 0");
                                break;
                            case TrendDirection.Sideways:
                                trainingFileValues.Add("0 1 0");
                                break;
                            case TrendDirection.Up:
                                trainingFileValues.Add("0 0 1");
                                break;
                        }

                        trainingFileValues.Add("");

                        addedPatterns++;
                        patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternDate, -exchangeDaysStep);
                    }

                    if (addedPatterns != desiredNumberOfPatterns)
                    {
                        trainingFileValues[0] = addedPatterns.ToString();
                        if (!File.Exists(dataPath + i.ToString() + " " + patternsSearchStartDate.ToShortDateString() + ".txt"))
                        {
                            File.AppendAllLines(dataPath + i.ToString() + " " + patternsSearchStartDate.ToShortDateString() + ".txt", trainingFileValues);
                        }

                        continue;
                    }
                    else
                    {
                        trainingFileValues[0] = desiredNumberOfPatterns.ToString();
                        File.AppendAllLines(dataPath + i.ToString() + " " + patternsSearchStartDate.ToShortDateString() + ".txt", trainingFileValues);
                    }
                }
            }
        }

        private void GenerateTrainingData(
            string dataPath,
            int minNumberOfPeriods,
            int maxNumberOfPeriods,
            int periodsStep,
            int minTrainingPatterns,
            int maxTrainingPatterns,
            int trainingPatternsStep,
            DateTime patternsSearchStartDate,
            int exchangeDaysStep
            )
        {
            patternsSearchStartDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternsSearchStartDate, 0);
            Directory.CreateDirectory(dataPath);
            for (int i = minNumberOfPeriods; i <= maxNumberOfPeriods; i += periodsStep)
            {
                List<string> trainingFileValues = new List<string>();
                trainingFileValues.Add("");
                trainingFileValues.Add((2 * i - 1).ToString());
                trainingFileValues.Add("3");
                trainingFileValues.Add("");

                int addedPatterns = 0;
                DateTime patternDate = patternsSearchStartDate;
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                for (int j = minTrainingPatterns; j <= maxTrainingPatterns; j += trainingPatternsStep)
                {
                    if (!File.Exists(dataPath + i.ToString() + " " + j.ToString() + " " + patternsSearchStartDate.ToShortDateString() + ".txt"))
                    {
                        while ((addedPatterns < j) && (patternDate > firstExchangeQuotationDate))
                        {
                            List<ExchangePeriod> historicalData = DataProvider.Instance.GetExchangePeriodsMergedByMovementDirectoryFromEndDate(i, patternDate, firstExchangeQuotationDate, 7);
                            if (historicalData.Count != i)
                            {
                                patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternDate, -exchangeDaysStep);
                                continue;
                            }

                            List<ExchangePeriod> resultData = DataProvider.Instance.GetExchangePeriodsMergedByMovementDirectoryFromStartDate(3, DateTime.Today, patternDate, 7);
                            if (resultData.Count != 3)
                            {
                                patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternDate, -exchangeDaysStep);
                                continue;
                            }

                            for (int h = 0; h < historicalData.Count; ++h)
                            {
                                trainingFileValues.Add(historicalData[h].PercentageChange.ToString(nfi));
                                if (h + 1 < historicalData.Count)
                                {
                                    trainingFileValues.Add(((historicalData[h + 1].PublicTrading - historicalData[h].PublicTrading) / historicalData[h].PublicTrading).ToString(nfi));
                                }
                            }

                            trainingFileValues.Add("");

                            TrendDirection direction = GetTrendDirection(resultData, 0);
                            switch (direction)
                            {
                                case TrendDirection.Down:
                                    trainingFileValues.Add("1 0 0");
                                    break;
                                case TrendDirection.Sideways:
                                    trainingFileValues.Add("0 1 0");
                                    break;
                                case TrendDirection.Up:
                                    trainingFileValues.Add("0 0 1");
                                    break;
                            }

                            trainingFileValues.Add("");

                            addedPatterns++;
                            patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternDate, -exchangeDaysStep);
                        }

                        if (addedPatterns != j)
                        {
                            trainingFileValues[0] = addedPatterns.ToString();
                            if (!File.Exists(dataPath + i.ToString() + " " + addedPatterns.ToString() + " " + patternsSearchStartDate.ToShortDateString() + ".txt"))
                            {
                                File.AppendAllLines(dataPath + i.ToString() + " " + addedPatterns.ToString() + " " + patternsSearchStartDate.ToShortDateString() + ".txt", trainingFileValues);
                            }

                            break;
                        }
                        else
                        {
                            trainingFileValues[0] = j.ToString();
                            File.AppendAllLines(dataPath + i.ToString() + " " + j.ToString() + " " + patternsSearchStartDate.ToShortDateString() + ".txt", trainingFileValues);
                        }
                    }
                }
            }
        }

        private TrendDirection GetTrendDirection(List<ExchangePeriod> periodList, int beginIndex)
        {
            double change = (periodList[beginIndex + 2].CloseRate - periodList[beginIndex].OpenRate) / periodList[beginIndex].OpenRate;
            if (change > 0.3 || change < 0.3)
            {
                if ((periodList[beginIndex].OpenRate < periodList[beginIndex + 1].CloseRate) && (periodList[beginIndex].CloseRate < periodList[beginIndex + 2].CloseRate))
                {
                    return TrendDirection.Up;
                }

                if ((periodList[beginIndex].OpenRate > periodList[beginIndex + 1].CloseRate) && (periodList[beginIndex].CloseRate > periodList[beginIndex + 2].CloseRate))
                {
                    return TrendDirection.Down;
                }
            }

            return TrendDirection.Sideways;
        }

        public PredictionResult PredictTrendDirection(DateTime date, string path)
        {
            path = Path.GetFileName(path);
            StringReader reader;
            try
            {
                reader = new StringReader(File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + path + "\\LowestMSENetwork.xml"));
            }
            catch
            {
                return null;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(NetworkMSE));
            NetworkMSE lowesMSE = (NetworkMSE)serializer.Deserialize(reader);
            NeuralNet net = new NeuralNet();
            if (!net.CreateFromFile(path + "\\" + lowesMSE.NetworkFileName));
            return null;
            uint numberOfInputs = net.GetNumInput();
            uint numberOfPeriods = (numberOfInputs + 1) / 2;
            List<ExchangePeriod> periods = DataProvider.Instance.GetExchangePeriodsMergedByMovementDirectoryFromEndDate((int)numberOfPeriods, date, this.firstExchangeQuotationDate, 7);
            double[] inputs = new double[numberOfInputs];
            for (int i = 0; i < numberOfPeriods; i += 2)
            {
                // trainingFileValues.Add(historicalData[h].PercentageChange.ToString(nfi));
                inputs[i] = periods[i].PercentageChange;
                if (i + 1 < periods.Count)
                {
                    inputs[i + 1] = (periods[i + 1].PublicTrading - periods[i].PublicTrading) / periods[i].PublicTrading;
                }
            }
            double[] result = net.Run(inputs);
            PredictionResult results = new PredictionResult();
            results.Trends = new List<TrendDirectionWithPropability>();
            double sum = 0;
            foreach (double r in result)
            {
                if (r > 0)
                {
                    sum += r;
                }
            }

            for (int i = 0; i < result.Length; i++)
            {
                TrendDirectionWithPropability td = new TrendDirectionWithPropability();
                if (result[i] > 0)
                {
                    td.Propability = result[i] * 100 / sum;
                }
                else
                {
                    td.Propability = 0;
                }

                switch (i)
                {
                    case 0:
                        td.Direction = TrendDirection.Down;
                        break;
                    case 1:
                        td.Direction = TrendDirection.Sideways;
                        break;
                    case 2:
                        td.Direction = TrendDirection.Up;
                        break;
                }
                results.Trends.Add(td);
            }

            //periods = DataProvider.Instance.GetExchangePeriodsMergedByMovementDirectoryFromStartDate((int)3, DateTime.Today, date, 7);

            //TrendDirection testTrueDirection = GetTrendDirection(periods, 0);
            int[] resultParam = DataProvider.Instance.GetNetParametersFromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + path + "\\" + lowesMSE.NetworkFileName);
            if (resultParam != null)
            {
                results.PeriodNo = resultParam[0];
                results.PatternsNo = resultParam[1];
                results.NeuronNo = resultParam[2];
                results.EpochsNo = resultParam[3];
            }
            results.MSE = lowesMSE.MSE;
            return results;

        }

        public DateTime GetNextExchangeQuotationDate(DateTime baseExchangeQuotationDate, int offset)
        {
            return DataProvider.Instance.GetNextExchangeQuotationDate(baseExchangeQuotationDate, offset);
        }
    }
}
