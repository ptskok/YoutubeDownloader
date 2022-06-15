using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeDownloaderCore;
using YoutubeExplode.Videos;

namespace YoutubeDownloaderConsole
{
    public class ConsoleUi
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Write full path, where do you want to save your file: ");
            string savePath = Console.ReadLine();

            try
            {
                Path.GetFullPath(savePath);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid path!");
                throw;
            }

            Console.WriteLine("Please, paste in the video/playlist link:");
            string videoLink = Console.ReadLine();

            try
            {
                VideoId.Parse(videoLink);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid link provided!");
                throw;
            }

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
            await downloader.Download(option);
        }
    }
}
