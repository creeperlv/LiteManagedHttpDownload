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
        public static string DownloadToFileWithProgressBuffered(string Url,string path,ref double Progress,int BufferS=-1)
        {
            if (BufferS == -1)
            {
                BufferS = BufferSize;
            }
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3952.0 Safari/537.36 Edg/80.0.320.3 LiteManagedHttpDownloader/1.0.0.0");
            var s= httpClient.GetAsync(Url);
            //var s =(httpClient.GetStreamAsync(Url));
            FileInfo fileInfo = new FileInfo(path);
            if(File.Exists(path))
            File.WriteAllText(path, "");
            var FW=fileInfo.OpenWrite();
            s.Wait();
            var message = s.Result;
            var sta = message.Content.ReadAsStreamAsync();
            sta.Wait();
            var st = sta.Result;
            byte[] b = new byte[BufferS];

            foreach (var item in message.Content.Headers)
            {
                Console.WriteLine($"{item.Key}");
                foreach (var i in item.Value)
                {
                    Console.WriteLine($"{i}");
                }
            }
            
            while (st.Position<st.Length)
            {
                int r=st.Read(b, 0, b.Length);
                //Console.WriteLine();
                FW.Write(b, 0, r);
                Progress = (((double)st.Position) / ((double)st.Length));
            }
            Progress = 1;
            st.Close();
            st.Dispose();
            FW.Close();
            FW.Dispose();
            return "SUCCESS";
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
