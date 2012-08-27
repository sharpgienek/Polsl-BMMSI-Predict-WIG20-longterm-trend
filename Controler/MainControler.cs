using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading;
using DTO;
using System.IO;
using System.Reflection;

namespace Controler
{
    public class MainControler
    {
        enum TrendDirection
        {
            Down = -1,
            Sideways = 0,
            Up = 1
        }

        private static readonly DateTime firstExchangeQuotationDate = new DateTime(1990, 4, 16);

        public static void DownloadData(Delegate updateProgress)        
        {            
            DataDownloader downloader = DataDownloader.Instance;
            
            Thread downloadThread = new Thread(new ThreadStart(downloader.DownloadData));
            downloadThread.Start();
            while (!downloadThread.IsAlive)
            {
            }

            while (downloader.DownloadProgress != 1)
            {
                Thread.Sleep(10);
                updateProgress.Method.Invoke(updateProgress.Target, new object[] { downloader.DownloadProgress});                
            }

            updateProgress.Method.Invoke(updateProgress.Target, new object[] { downloader.DownloadProgress });

            string trainingDataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\training data\\";
            for (int i = 8; i < 9; i++)
            {
                List<string> trainingFileValues = new List<string>();
                trainingFileValues.Add("");
                trainingFileValues.Add((2 * i - 1).ToString());
                trainingFileValues.Add("3");
                trainingFileValues.Add("");

                int addedPatterns = 0;
                DateTime patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(DateTime.Today, -3);
                for (int j = 10; j < 200; j += 5)
                {                    
                    if (!File.Exists(trainingDataPath + i.ToString() + "-" + j.ToString() + ".txt"))
                    {
                        trainingFileValues[0] = j.ToString();
                        while((addedPatterns < j) && (patternDate > firstExchangeQuotationDate))
                        {
                            List<ExchangePeriod> historicalData = DataProvider.Instance.GetExchangePeriodsMergedByMovementDirectoryFromEndDate(i, patternDate, firstExchangeQuotationDate, 1);
                            if (historicalData.Count != i)
                            {
                                patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternDate, -2);
                                continue;
                            }

                            List<ExchangePeriod> resultData = DataProvider.Instance.GetExchangePeriodsMergedByMovementDirectoryFromStartDate(3, DateTime.Today, patternDate, 1);
                            if (resultData.Count != 3)
                            {
                                patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternDate, -2);
                                continue;
                            }

                            for (int h = 0; h < historicalData.Count; ++h)
                            {
                                trainingFileValues.Add(historicalData[h].PercentageChange.ToString());
                                if (h + 1 < historicalData.Count)
                                {
                                    trainingFileValues.Add(((historicalData[h + 1].PublicTrading - historicalData[h].PublicTrading) / historicalData[h].PublicTrading).ToString());
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
                            patternDate = DataProvider.Instance.GetNextExchangeQuotationDate(patternDate, -2);
                        }

                        File.AppendAllLines(trainingDataPath + i.ToString() + "-" + j.ToString() + ".txt", trainingFileValues);
                    }
                    
                }
            }
            
        }

        private static TrendDirection GetTrendDirection(List<ExchangePeriod> periodList, int beginIndex)
        {
            if ((periodList[beginIndex].OpenRate < periodList[beginIndex + 1].CloseRate) && (periodList[beginIndex].CloseRate < periodList[beginIndex + 2].CloseRate))
            {
                return TrendDirection.Up;
            }

            if ((periodList[beginIndex].OpenRate > periodList[beginIndex + 1].CloseRate) && (periodList[beginIndex].CloseRate > periodList[beginIndex + 2].CloseRate))
            {
                return TrendDirection.Down;
            }

            return TrendDirection.Sideways;
        }
    }
}
