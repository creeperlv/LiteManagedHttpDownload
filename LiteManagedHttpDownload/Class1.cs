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
        public static void DownloadToFileWithProgressAsync(string Url,string path,ref double Progress)
        {
            HttpClient httpClient = new HttpClient();
            var s=(httpClient.GetStreamAsync(Url));
            FileInfo fileInfo = new FileInfo(path);
            var FW=fileInfo.OpenWrite();
            s.Wait();
            var st = s.Result;
            byte[] b = new byte[64];
            while (st.Position<st.Length)
            {
                st.Read(b, 0, b.Length);
                FW.Write(b, 0, b.Length);
                Progress = (double)st.Position / (double)st.Length;
            }

            //st.EndRead();
            st.Close();
            st.Dispose();
            FW.Close();
            FW.Dispose();
        }
        public static async void DownloadToFileFromBase64Async(string Url,string path)
        {
            HttpClient httpClient = new HttpClient();
            var s=Convert.FromBase64String(await httpClient.GetStringAsync(Url));
            File.WriteAllBytes(path, s);
        }
    }
}
