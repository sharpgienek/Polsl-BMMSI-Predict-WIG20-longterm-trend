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
    /// <summary>
    /// Singleton odpowiedzialny za dostarczenie metod związanych z pobieraniem danych z internetu.
    /// </summary>
    public class DataDownloader
    {
        private static DataDownloader instance;

        /// <summary>
        /// Instancja reprezentująca singleton.
        /// </summary>
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

        /// <summary>
        /// Ścieżka zapisu danych historycznych pobieranych z internetu.
        /// </summary>
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

        /// <summary>
        /// Lista dat notowań.
        /// </summary>
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

        /// <summary>
        /// Data pierwszego notowania.
        /// </summary>
        public DateTime FirstStockExchangeQuotationDate { get; set; }

        /// <summary>
        /// Zdarzenie wywoływane, gdy pobieranie zostanie zakończone.
        /// </summary>
        public event EventHandler DownloadComplete;

        private string downloadStatusAssignOnlyThruPropertySetter;

        /// <summary>
        /// Stan pobierania.
        /// </summary>
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

        /// <summary>
        /// Zdarzenie wywoływane, gdy postęp pobierania się zmieni.
        /// </summary>
        public event EventHandler<DownloadProgressChangedDTOEventArgs> DownloadProgressChanged;

        private List<DateTime> datesToDownload;

        /// <summary>
        /// Postęp pobierania wyrażony w procentach.
        /// </summary>
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
        
        /// <summary>
        /// Prywatny konstruktor.
        /// </summary>
        private DataDownloader()
        {
            this.DataDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\data\\";
            Directory.CreateDirectory(this.DataDirectory);
        }
        
        /// <summary>
        /// Metoda pobierania danych historycznych.
        /// </summary>
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
                EventHandler<DownloadProgressChangedDTOEventArgs> temp = this.DownloadProgressChanged;
                if (temp != null)
                {
                    temp.Invoke(this, new DownloadProgressChangedDTOEventArgs()
                    {
                        Progress = 100
                    });
                }
            }
        }

        /// <summary>
        /// Metoda pobierania pliku asynchronicznie.
        /// </summary>
        /// <param name="address">Adres do pobrania.</param>
        /// <param name="path">Ścieżka do zapisania.</param>
        private void DownloadFileAsync(string address, string path)
        {
            using (WebClient webClient = new WebClient())
            {
                Uri uri = new Uri(address);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDownloaded);
                webClient.DownloadFileAsync(uri, path);
            }
        }

        /// <summary>
        /// Metoda pobierania pliku.
        /// </summary>
        /// <param name="address">Adres do pobrania.</param>
        /// <param name="path">Ścieżka do zapisania.</param>
        public void DownloadFile(string address, string path)
        {
            using (WebClient webClient = new WebClient())
            {
                Uri uri = new Uri(address);
                webClient.DownloadFile(uri, path);
            }
        }

        /// <summary>
        /// Metoda wywoływana, gdy zostanie pobrany plik danych historycznych.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileDownloaded(object sender, AsyncCompletedEventArgs e)
        {
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

        /// <summary>
        /// Metoda usuwania błędnych plików danych historycznych. Aktualnie nie używana, trwa bardzo długo.
        /// </summary>
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

        /// <summary>
        /// Metoda pobierania dat notowań.
        /// </summary>
        /// <returns>Lista dat notowań.</returns>
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
