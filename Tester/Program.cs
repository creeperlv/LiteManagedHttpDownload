using LiteManagedHttpDownload;
using System;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            double progress = 0.0f;
            Downloader.DownloadToFileWithProgressBuffered("https://github.com/xenko3d/xenko/tree/master/sources", "./Download", ref progress, 65536);
            Downloader.DownloadToFileAsync("https://github.com/xenko3d/xenko/tree/master/sources", "./Download2").Wait();
        }
    }
}
