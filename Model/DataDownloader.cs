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
using DTO.DTOEventArgs;

namespace Model
{
    public class DataDownloader
    {
        private static DataDownloader instance;

        public static DataDownloader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataDownloader();
                }

                return instance;
            }
        }

        private int toDownload;

        private int numberOfDownloadedFilesAssignOnlyInPropertySetter;

        public string DataDirectory { get; set; }

        private int NumberOfDownloadedFiles
        {
            get
            {
                return this.numberOfDownloadedFilesAssignOnlyInPropertySetter;
            }

            set
            {
                int oldValue = this.numberOfDownloadedFilesAssignOnlyInPropertySetter;

                if (value >= this.toDownload)
                {
                    this.numberOfDownloadedFilesAssignOnlyInPropertySetter = this.toDownload;
                }
                else
                {
                    if (value <= 0)
                    {
                        this.numberOfDownloadedFilesAssignOnlyInPropertySetter = 0;
                    }
                    else
                    {
                        this.numberOfDownloadedFilesAssignOnlyInPropertySetter = value;
                    }
                }

                if (this.numberOfDownloadedFilesAssignOnlyInPropertySetter != oldValue)
                {
                    EventHandler<DownloadProgressChangedDTOEventArgs> temp = this.DownloadProgressChanged;
                    if (temp != null)
                    {
                        temp.Invoke(this, new DownloadProgressChangedDTOEventArgs()
                        {
                            Progress = this.DownloadProgress
                        });
                    }

                    if (this.numberOfDownloadedFilesAssignOnlyInPropertySetter == this.toDownload)
                    {
                        this.DownloadStatus = "Download complete.";
                    }
                }
            }
        }
        
        private List<DateTime> exchangeDates;

        public List<DateTime> ExchangeDates
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

        public event EventHandler DownloadComplete;

        private string downloadStatusAssignOnlyThruPropertySetter;

        public string DownloadStatus
        {
            get
            {
                return this.downloadStatusAssignOnlyThruPropertySetter;
            }

            set
            {
                this.downloadStatusAssignOnlyThruPropertySetter = value;
                if (value.Equals("Download complete."))
                {
                    EventHandler tmp = this.DownloadComplete;
                    if (tmp != null)
                    {
                        tmp.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public event EventHandler<DownloadProgressChangedDTOEventArgs> DownloadProgressChanged;

        private List<DateTime> datesToDownload;

        public double DownloadProgress
        {
            get
            {
                if (this.toDownload == 0)
                {
                    return 100;
                }
                
                return this.NumberOfDownloadedFiles * 100 / this.toDownload;
            }
        }
        
        private DataDownloader()
        {
            this.DataDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
        }
        
        public void DownloadData()
        {    
            this.DownloadStatus = "Initializing download.";
            this.toDownload = this.ExchangeDates.Count;            
            this.datesToDownload = new List<DateTime>();
            foreach(DateTime date in this.ExchangeDates)
            {
                if (File.Exists(this.DataDirectory + date.ToShortDateString() + "_indeksy.xls"))
                {
                    continue;
                }

                this.datesToDownload.Add(date);
            }

            this.toDownload = this.datesToDownload.Count;

            if (this.toDownload != 0)
            {
                this.DownloadStatus = "Download in progress.";

                List<DateTime> initialDownloadRange;
                 if (this.datesToDownload.Count >= 10)
                 {
                     initialDownloadRange = this.datesToDownload.GetRange(this.datesToDownload.Count - 10, 10);
                     this.datesToDownload.RemoveRange(this.datesToDownload.Count - 10, 10);
                 }
                 else
                 {
                     initialDownloadRange = this.datesToDownload.GetRange(0, this.datesToDownload.Count);
                     this.datesToDownload.RemoveRange(0, this.datesToDownload.Count);
                 }                

                foreach (DateTime date in initialDownloadRange)
                {
                    DownloadFileAsync(
                        "http://www.gpw.pl/notowania_archiwalne?type=1&date=" + date.ToShortDateString() + "&fetch.x=25&fetch.y=18",
                        this.DataDirectory + date.ToShortDateString() + "_indeksy.xls"
                        );
                }
            }
            else
            {
                this.DownloadStatus = "Download complete.";
            }
        }

        private void DownloadFileAsync(string address, string path)
        {
            using (WebClient webClient = new WebClient())
            {
                Uri uri = new Uri(address);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDownloaded);
                webClient.DownloadFileAsync(uri, path);
            }
        }

        private void FileDownloaded(object sender, AsyncCompletedEventArgs e)
        {//todo co gdy e.error?
            this.NumberOfDownloadedFiles++;
            if (this.datesToDownload != null && this.datesToDownload.Count != 0)
            {
                DateTime date = this.datesToDownload.Last();
                DownloadFileAsync(
                         "http://www.gpw.pl/notowania_archiwalne?type=1&date=" + date.ToShortDateString() + "&fetch.x=25&fetch.y=18",
                         this.DataDirectory + this.datesToDownload.Last().ToShortDateString() + "_indeksy.xls"
                         );
                this.datesToDownload.Remove(date);
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
                    catch
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
                //todo co gdy błąd
                string datesText = client.DownloadString("http://www.gpw.pl/notowania_archiwalne");
                datesText = datesText.Substring(datesText.IndexOf("calendarEnabledDates = {'") + "calendarEnabledDates = {'".Length);
                datesText = datesText.Remove(datesText.IndexOf("':1}"));
                datesText = datesText.Replace("':1,'", ",");
                string[] datesArray = datesText.Split(',');                
                foreach (string date in datesArray)
                {
                    string[] dateParts = date.Split('-');
                    result.Insert(0,(new DateTime(Convert.ToInt32(dateParts[0]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]))));
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
