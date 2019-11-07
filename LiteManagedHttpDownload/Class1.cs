using System;
using System.IO;
using System.Net.Http;

namespace LiteManagedHttpDownload
{
    public class Downloader
    {
        public static async void DownloadToFileAsync(string Url,string path)
        {
            HttpClient httpClient = new HttpClient();
            var s=await httpClient.GetByteArrayAsync(Url);
            File.WriteAllBytes(path, s);
        }
    }
}
