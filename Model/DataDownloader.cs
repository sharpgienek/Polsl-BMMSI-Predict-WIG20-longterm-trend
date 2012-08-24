using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Data.OleDb;
using System.Reflection;
using System.Threading;

namespace Model
{
    public class DataDownloader
    {
        private int toDownload;

        private int downloaded;

        private double downloadProgress;

        private List<DateTime> exchangeDates;

        private List<DateTime> ExchangeDates
        {
            get
            {
                if (exchangeDates == null)
                {
                    exchangeDates = DownloadExchangeDates();
                }

                return exchangeDates;
            }
        }

        public DateTime FirstStockExchangeQuotationDate { get; set; }

        public string DownloadStatus { get; set; }

        public double DownloadProgress
        {
            get 
            {
                return downloadProgress;
            }
        }
        
        public DataDownloader()
        {
        }

        public void DownloadData()
        {    
            this.DownloadStatus = "Initializing download.";
            DeleteIncorrectFiles();
            this.toDownload = this.ExchangeDates.Count;
            string dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
            this.DownloadStatus = "Download in progress.";
            int i = 0;
            foreach(DateTime date in this.ExchangeDates)
            {
                if (File.Exists(dataPath + date.ToShortDateString() + "_indeksy.xls"))
                {
                    this.toDownload--;
                    continue;
                }
                    
                String address = "http://www.gpw.pl/notowania_archiwalne?type=1&date=" + date.ToShortDateString() + "&fetch.x=25&fetch.y=18";

                using (WebClient webClient = new WebClient())
                {
                    i++;
                    Uri uri = new Uri(address);
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDownloaded);
                    webClient.DownloadFileAsync(uri, dataPath + date.ToShortDateString() + "_indeksy.xls");
                }

                while (this.downloaded + 10 < i)
                {
                    Thread.Sleep(10);
                }

            }
            
            if (this.toDownload == this.downloaded)
            {
                this.DownloadStatus = "Download complete.";
                this.downloadProgress = 1;
            }
        }

        private void FileDownloaded(object sender, AsyncCompletedEventArgs e)
        {
            this.downloaded++;
            if ((this.downloaded / this.toDownload) > 0.01)
            {
                this.downloadProgress = (this.downloaded / this.toDownload) - 0.01;
            }

            if (this.downloadProgress == 0.99)
            {
                this.DownloadStatus = "Finalizing download.";
                DeleteIncorrectFiles();
                this.DownloadStatus = "Download complete.";
                this.downloadProgress = 1;
            }
        }

        private void DeleteIncorrectFiles()
        {
            string dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
            foreach (DateTime date in this.ExchangeDates)
            {
                //Sprawdzenie, czy plik istnieje, lub data odnosi się do dni, w których nie ma notowań.
                if (File.Exists(dataPath + date.ToShortDateString() + "_indeksy.xls"))
                {
                    String connectionString = @"Provider=Microsoft.Jet.OleDb.4.0; data source=" + dataPath + date.ToShortDateString() + @"_indeksy.xls; Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";
                    OleDbConnection con = new OleDbConnection(connectionString);
                    try
                    {
                        con.Open();
                        con.Close();
                    }
                    catch (OleDbException e)
                    {
                        File.Delete(dataPath + date.ToShortDateString() + "_indeksy.xls");
                    }
                }
            }
        }

        private List<DateTime> DownloadExchangeDates()
        {
            List<DateTime> result = new List<DateTime>();
            using (WebClient client = new WebClient())
            {
                string datesText = client.DownloadString("http://www.gpw.pl/notowania_archiwalne");
                datesText = datesText.Substring(datesText.IndexOf("calendarEnabledDates = {'") + "calendarEnabledDates = {'".Length);
                datesText = datesText.Remove(datesText.IndexOf("':1}"));
                datesText = datesText.Replace("':1,'", ",");
                string[] datesArray = datesText.Split(',');                
                foreach (string date in datesArray)
                {
                    string[] dateParts = date.Split('-');
                    result.Add(new DateTime(Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2])));
                }                
            }
            if (result.Count == 0)
            {
                return null;
            }
            return result;
        }
    }
}
