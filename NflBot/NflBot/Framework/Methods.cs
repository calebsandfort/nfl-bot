using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace NflBot.Framework
{
    public static class Methods
    {
        #region Public - DownloadPageStringAsync
        public async static Task<String> DownloadPageStringAsync(String url)
        {
            using (WebClient wc = new WebClient())
            {
                bool failed = false;

                try
                {
                    return await wc.DownloadStringTaskAsync(url);
                }
                catch (WebException)
                {
                    failed = true;
                }

                if (failed)
                {
                    try
                    {
                        Thread.Sleep(2500);
                        return await wc.DownloadStringTaskAsync(url);
                    }
                    catch (WebException)
                    {
                        return String.Empty;
                    }
                }

                return String.Empty;
            }
        }
        #endregion
    }
}