using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.IO;
using System.Reflection;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Model
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }

                return instance;
            }
        }

        private List<ExchangeDay> exchangeDays;

        private DataProvider()
        {
           // InitializeExchangeDaysFromXls();
            InitializeExchangeDays();
        }

        private void SaveExchangeDays(string path)
        {
            StringWriter writer = new StringWriter();

            XmlSerializer serializer = new XmlSerializer(typeof(List<ExchangeDay>));
            serializer.Serialize(writer, this.exchangeDays);

            File.WriteAllText(path, writer.ToString());
        }

        private List<ExchangeDay> GetExchangeDaysFromXls()
        {
            this.exchangeDays = new List<ExchangeDay>();
            string dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
            List<ExchangeDay> results = new List<ExchangeDay>();
            foreach (DateTime date in DataDownloader.Instance.ExchangeDates)
            {
                ExchangeDay day = GetExchangeDayFromXls(date);
                if (day != null)
                {
                    results.Add(day);
                }                
            }

            if (results.Count != 0)
            {
                return results;
            }

            return null;
        }

        private void InitializeExchangeDays()
        {
            this.exchangeDays = GetExchangeDaysFromXml(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\exchange days.xml");
            bool needToSaveNewXml = false;
            if (this.exchangeDays != null)
            {
                if (this.exchangeDays.Last().Date != DataDownloader.Instance.ExchangeDates.Last())
                {
                    needToSaveNewXml = true;
                    do
                    {
                        ExchangeDay day = GetExchangeDayFromXls(GetNextExchangeQuotationDate(this.exchangeDays.Last().Date, 1));
                        if (day != null)
                        {
                            this.exchangeDays.Add(day);
                        }
                        else
                        {
                            //todo wtf? :P
                        }

                    } while (this.exchangeDays.Last().Date != DataDownloader.Instance.ExchangeDates.Last());
                }

                if (needToSaveNewXml)
                {
                    SaveExchangeDays(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\" + "exchange days.xml");
                }
            }
            else
            {
                this.exchangeDays = GetExchangeDaysFromXls();
                if (this.exchangeDays == null)
                {
                    this.exchangeDays = new List<ExchangeDay>();
                }
                else
                {
                    SaveExchangeDays(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\" + "exchange days.xml");
                }
            }
        }

        private List<ExchangeDay> GetExchangeDaysFromXml(string path)
        {
            //todo try/catch?
            try
            {
                StringReader reader = new StringReader(File.ReadAllText(path));

                XmlSerializer serializer = new XmlSerializer(typeof(List<ExchangeDay>));
                return (List<ExchangeDay>)serializer.Deserialize(reader);
            }
            catch
            {
                return null;
            }
        }

        public ExchangePeriod GetExchangePeriod(DateTime startDate, DateTime endDate)
        {
            DateTime checkedDate = startDate.Date;
            ExchangePeriod result = null;
            ExchangePeriod checkedPeriod;
            while (checkedDate <= endDate)
            {
                checkedPeriod = GetExchangeDay(checkedDate);
                if (checkedPeriod != null)
                {
                    if (result == null)
                    {
                        result = checkedPeriod;
                    }
                    else
                    {
                        result.PublicTrading += checkedPeriod.PublicTrading;
                        if (checkedDate == endDate)
                        {
                            result.CloseRate = checkedPeriod.CloseRate;
                            result.PeriodEnd = checkedPeriod.PeriodEnd;
                        }
                    }
                }

                checkedDate = checkedDate.AddDays(1);
            }

            return result;
        }

        public ExchangeDay GetExchangeDay(DateTime date)
        {
            //todo dwa ostatnie elementy this.exchangeDays się powtarzają..
            return this.exchangeDays.SingleOrDefault(d => d.Date == date);
        }

        private static ExchangeDay GetExchangeDayFromXls(DateTime date)
        {
            string dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
            string test = dataPath + date.Date.ToShortDateString() + "_indeksy.xls";
            if (File.Exists(dataPath + date.Date.ToShortDateString() + "_indeksy.xls"))
            {
                String connectionString = @"Provider=Microsoft.Jet.OleDb.4.0; data source=" + dataPath + date.Date.ToShortDateString() + @"_indeksy.xls; Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand excelCommand = new OleDbCommand(
                        "SELECT Obrót, `Kurs zamknięcia` " +
                        "FROM [Worksheet$] " +
                        "WHERE Nazwa = 'WIG20'", connection))
                    {
                        using (OleDbDataReader reader = excelCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return (new ExchangeDay()
                                {
                                    CloseRate = (double)reader["Kurs zamknięcia"],
                                    PublicTrading = (double)reader["Obrót"],
                                    Date = date
                                });
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }

            return null;
        }

        private double GetPublicTrading(DateTime date)
        {
            string dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
            if (File.Exists(dataPath + date.Date.ToShortDateString() + "_indeksy.xls"))
            {
                String connectionString = @"Provider=Microsoft.Jet.OleDb.4.0; data source=" + dataPath + date.Date.ToShortDateString() + @"_indeksy.xls; Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand excelCommand = new OleDbCommand(
                        "SELECT Obrót " +
                        "FROM [Worksheet$] " +
                        "WHERE Nazwa = 'WIG20'", connection))
                    {
                        using (OleDbDataReader reader = excelCommand.ExecuteReader())
                        {
                            reader.Read();
                            return (double)reader["Obrót"];
                        }
                    }
                }
            }
            else
            {
                return -1;
            }
        }

        public List<ExchangePeriod> GetExchangePeriodsMergedByMovementDirectoryFromEndDate(int DesiredNumberOfPeriods, DateTime periodsEndDate, DateTime periodsStartDate, int daysInterval)
        {
            DateTime iterationDate = periodsEndDate.Date;
            string dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
            List<ExchangePeriod> periodList = new List<ExchangePeriod>();
            while (iterationDate >= periodsStartDate)
            {
                DateTime periodStart;
                if (iterationDate.AddDays(-daysInterval) > periodsStartDate)
                {
                    periodStart = iterationDate.AddDays(-daysInterval);
                }
                else
                {
                    periodStart = periodsStartDate.Date;
                }

                ExchangePeriod period = GetExchangePeriod(periodStart, iterationDate);
                if (period != null)
                {
                    
                    period.PublicTrading -= GetExchangeDay(period.PeriodStart).PublicTrading;
                    if (periodList.Count != 0)
                    {
                        periodList.First().OpenRate = period.CloseRate;
                        
                        //If percentage changes have the same sign.
                        if ((periodList.First().PercentageChange * period.PercentageChange) > 0 || periodList.First().PeriodStart == periodList.First().PeriodEnd)
                        {
                            periodList.First().PublicTrading += period.PublicTrading;
                            if (period.PeriodEnd != periodList.First().PeriodStart)
                            {
                                periodList.First().PublicTrading += GetExchangeDay(periodList.First().PeriodStart).PublicTrading;
                            }

                            periodList.First().PeriodStart = period.PeriodStart;
                            if ((periodList.Count > 1) && ((periodList[0].PercentageChange * periodList[1].PercentageChange) > 0))
                            {
                                periodList[1].OpenRate = periodList[0].OpenRate;
                                periodList[1].PeriodStart = periodList[0].PeriodStart;
                                periodList[1].PublicTrading += periodList[0].PublicTrading + GetExchangeDay(periodList[1].PeriodStart).PublicTrading;
                                periodList.RemoveAt(0);
                            }
                        }
                        else
                        {
                            if (periodList.Count == DesiredNumberOfPeriods + 1)
                            {
                                periodList.RemoveAt(0);
                                break;
                            }

                            periodList.Insert(0, period);
                        }
                    }
                    else
                    {
                        periodList.Add(period);
                    }
                }

                iterationDate = iterationDate.AddDays(-daysInterval);
            }

            return periodList;
        }

        public List<ExchangePeriod> GetExchangePeriodsMergedByMovementDirectoryFromStartDate(int DesiredNumberOfPeriods, DateTime periodsEndDate, DateTime periodsStartDate, int daysInterval)
        {
            DateTime iterationDate = periodsStartDate.Date;
            string dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
            List<ExchangePeriod> periodList = new List<ExchangePeriod>();
            while (periodList.Count <= DesiredNumberOfPeriods && iterationDate <= periodsEndDate)
            {
                DateTime periodEnd;
                if (iterationDate.AddDays(daysInterval) < periodsEndDate)
                {
                    periodEnd = iterationDate.AddDays(daysInterval);
                }
                else
                {
                    periodEnd = periodsEndDate.Date;
                }

                ExchangePeriod period = GetExchangePeriod(iterationDate, periodEnd);
                if (period != null)
                {
                    if (periodList.Count != 0)
                    {
                        if (period.PeriodStart == periodList.Last().PeriodEnd)
                        {
                            period.PublicTrading -= GetExchangeDay(periodList.Last().PeriodEnd).PublicTrading;
                        }

                        //If percentage changes have the same sign.
                        if ((periodList.Last().PercentageChange * period.PercentageChange) >= 0)
                        {
                            periodList.Last().PeriodEnd = period.PeriodEnd;
                            periodList.Last().PublicTrading += period.PublicTrading;
                            periodList.Last().CloseRate = period.CloseRate;
                        }
                        else
                        {
                            period.OpenRate = periodList.Last().CloseRate;
                            periodList.Add(period);
                        }
                    }
                    else
                    {

                        period.PublicTrading -= GetExchangeDay(period.PeriodStart).PublicTrading;
                        periodList.Add(period);
                    }
                }

                iterationDate = iterationDate.AddDays(daysInterval);
            }

            while (periodList.Count > DesiredNumberOfPeriods)
            {
                periodList.Remove(periodList.Last());
            }

            return periodList;
        }

        public DateTime GetNextExchangeQuotationDate(DateTime baseExchangeQuotationDate, int offset)
        {
            DataDownloader downloader = DataDownloader.Instance;
            int indexOfClosestDate = -1;
            for (int i = downloader.ExchangeDates.Count - 1; i >= 0; --i)
            {
                if (downloader.ExchangeDates[i] <= baseExchangeQuotationDate)
                {
                    indexOfClosestDate = i;
                    break;
                }
            }

            if (indexOfClosestDate == -1)
            {
                return new DateTime(1, 1, 1);
            }

            try
            {
                return downloader.ExchangeDates[indexOfClosestDate + offset];
            }
            catch
            {
                return new DateTime(1, 1, 1);
            }
        }

        public List<TrainingDataFileParameters> GetTrainingDataFilesParameters(string trainingDataDirectoryPath)
        {
            List<TrainingDataFileParameters> result = new List<TrainingDataFileParameters>();
            string[] filePaths = Directory.GetFiles(trainingDataDirectoryPath, "*.txt");
            foreach (string filePath in filePaths)
            {
                try
                {
                    string fileName = (Path.GetFileName(filePath));
                    string fileNameWithoutExtension = fileName.Remove(fileName.Length - 4);
                    string[] fileNameParts = fileNameWithoutExtension.Split(' ');
                    string[] dateParts = fileNameParts[2].Split('-');
                    result.Add(new TrainingDataFileParameters()
                    {
                        FileName = fileName,
                        KeyDate = new DateTime(Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2])),
                        NumberOfPeriods = Convert.ToInt32(fileNameParts[0]),
                        NumberOfPatterns = Convert.ToInt32(fileNameParts[1])
                    });
                }
                catch
                {
                }
            }

            return result;
        }

        public int[] GetNetParametersFromFile(string fileName)
        {
            int[] result = new int[4];
            fileName = Path.GetFileName(fileName);
            try
            {
                string fileNameWithoutExtension = fileName.Remove(fileName.Length - 4);
                string[] fileNameParts = fileNameWithoutExtension.Split(' ');
                result[0] = Convert.ToInt32(fileNameParts[0]);
                result[1] = Convert.ToInt32(fileNameParts[1]);
                result[2] = Convert.ToInt32(fileNameParts[3]);
                result[3] = Convert.ToInt32(fileNameParts[4]);
                return result;
            }
            catch
            {
                return null;
            }

        }
    }
}
