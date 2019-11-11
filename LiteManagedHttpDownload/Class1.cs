using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace LiteManagedHttpDownload
{
    public class Downloader
    {

        public static async Task<string> DownloadToFileAsync(string Url,string path)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3952.0 Safari/537.36 Edg/80.0.320.3 LiteManagedHttpDownloader/1.0.0.0");
            try
            {

                var s = await httpClient.GetByteArrayAsync(Url);
                File.WriteAllBytes(path, s);
                return "SUCCESS";
            }
            catch (Exception e)
            {
                return ""+e.Message;
            }
        }
        public static int BufferSize=1024;
        public static void DownloadToFileWithProgressAsync(string Url,string path,ref double Progress)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3952.0 Safari/537.36 Edg/80.0.320.3 LiteManagedHttpDownloader/1.0.0.0");
            var s =(httpClient.GetStreamAsync(Url));
            FileInfo fileInfo = new FileInfo(path);
            var FW=fileInfo.OpenWrite();
            s.Wait();
            var st = s.Result;
            byte[] b = new byte[BufferSize];
            while (st.Position<st.Length)
            {
                st.Read(b, 0, b.Length);
                FW.Write(b, 0, b.Length);
                Progress = (double)st.Position / (double)st.Length;
            }
            st.Close();
            st.Dispose();
            FW.Close();
            FW.Dispose();
        }
        public static async void DownloadToFileFromBase64Async(string Url,string path)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3952.0 Safari/537.36 Edg/80.0.320.3 LiteManagedHttpDownloader/1.0.0.0");
            var s =Convert.FromBase64String(await httpClient.GetStringAsync(Url));
            File.WriteAllBytes(path, s);
        }
    }
}
