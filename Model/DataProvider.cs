using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.IO;
using System.Reflection;
using System.Data.OleDb;

namespace Model
{
    public class DataProvider
    {
        public static ExchangePeriod GetExchangePeriod(DateTime startDate, DateTime endDate)
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
                        result.OpenRate = result.CloseRate;
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

        public static ExchangePeriod GetExchangeDay(DateTime date)
        {
            string dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
            if (File.Exists(dataPath + date.Date.ToShortDateString() + "_indeksy.xls"))
            {
                String connectionString = @"Provider=Microsoft.Jet.OleDb.4.0; data source=" + dataPath + date.Date.ToShortDateString() + @"_indeksy.xls; Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand excelCommand = new OleDbCommand(
                        "SELECT Zmiana, Obrót, `Kurs otwarcia` , `Kurs zamknięcia` " +
                        "FROM [Worksheet$] " +
                        "WHERE Nazwa = 'WIG20'", connection))
                    {
                        using (OleDbDataReader reader = excelCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ExchangePeriod()
                                {
                                    OpenRate = (double)reader["Kurs otwarcia"],
                                    CloseRate = (double)reader["Kurs zamknięcia"],
                                    PeriodStart = date,
                                    PeriodEnd = date,
                                    PublicTrading = (double)reader["Obrót"]
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            else
            {
                return null;
            }
        }

        private static double GetPublicTrading(DateTime date)
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

        public static List<ExchangePeriod> GetExchangePeriodsMergedByMovementDirectoryFromEndDate(int DesiredNumberOfPeriods, DateTime periodsEndDate, DateTime periodsStartDate, int daysInterval)
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
                            periodList.First().PeriodStart = period.PeriodStart;
                            periodList.First().PublicTrading += period.PublicTrading;
                            if ((periodList[0].PercentageChange * periodList[1].PercentageChange) > 0)
                            {
                                periodList[1].OpenRate = periodList[0].OpenRate;
                                periodList[1].PeriodStart = periodList[0].PeriodStart;
                                periodList[1].PublicTrading += periodList[0].PublicTrading;
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

        public static List<ExchangePeriod> GetExchangePeriodsMergedByMovementDirectoryFromStartDate(int DesiredNumberOfPeriods, DateTime periodsEndDate, DateTime periodsStartDate, int daysInterval)
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
    }
}
