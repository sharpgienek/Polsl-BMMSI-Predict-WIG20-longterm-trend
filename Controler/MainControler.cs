using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading;
using DTO;

namespace Controler
{
    public class MainControler
    {
        public static void DownloadData(Delegate updateProgress)        
        {
            
            DataDownloader downloader = new DataDownloader();
            
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

            DataProvider.GetExchangePeriodsMergedByMovementDirectory(5, new DateTime(2012, 8, 12), new DateTime(1990, 1, 1), 1);
        }
    }
}
