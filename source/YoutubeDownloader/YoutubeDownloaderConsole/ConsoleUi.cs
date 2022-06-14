using System;
using YoutubeDownloaderCore;

namespace YoutubeDownloaderConsole
{
    public class ConsoleUi
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write full path, where do you want to save your file: ");
            string savePath = Console.ReadLine();
            Console.WriteLine("Please, paste in the video/playlist link:");
            string videoLink = Console.ReadLine();
            Console.WriteLine("Do you want to download audio, video or a playlist (videos,audios)?");
            Console.WriteLine("Please, write following character, depending on a preferable download option:");
            Console.WriteLine("a - only audio\nb - video\nc - playlist of videos\nd - playlist of audios" +
                              "\nany other key to exit program");
            var option = Console.Read();
            if (option < 'a' || option > 'd')
            {
                Environment.Exit(0);
            }
            var downloader = new Downloader(savePath, videoLink);
            downloader.Download(option);
        }
    }
}
