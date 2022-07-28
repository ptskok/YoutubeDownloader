using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeDownloaderCore;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace YoutubeDownloaderConsole
{
    public class ConsoleUi
    {
        private static double _progressBarVal;
        static async Task Main(string[] args)
        {
            _progressBarVal = 0;

            //PATH is predefined for performance purposes
            /*
            Console.WriteLine("Write full path, where do you want to save your file: ");
            string savePath = Console.ReadLine();*/
            string savePath = "C:\\Users\\ptsko\\OneDrive\\Počítač\\testGathers";

            try
            {
                Path.GetFullPath(savePath);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid path!");
                throw;
            }


            
            Console.WriteLine("Do you want to download audio, video or a playlist (videos,audios)?");
            Console.WriteLine("Please, write following character, depending on a preferable download option:");
            Console.WriteLine("a - only audio\nb - video\nc - playlist of videos\nd - playlist of audios" +
                              "\nany other key to exit program");

            var readString = Console.ReadLine();
            var option = 0;
            if (readString != "")
            {
                option = readString[0];
            }
            

            if (option < 'a' || option > 'd')
            {
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            }
            Console.WriteLine("Please, paste in the video/playlist link:");
            string videoLink = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case 'a':
                    case 'b':
                        //check if provided link is video
                        VideoId.Parse(videoLink);
                        break;
                    case 'c':
                    case 'd':
                        //check if provided link is playlist
                        PlaylistId.Parse(videoLink);
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid link provided!");
                throw;
            }

            var downloader = new Downloader(savePath, videoLink);
            try
            {
                var progress = new Progress<double>(value => _progressBarVal = value);
                var f =  downloader.Download(option,progress);
                ProgressBarUpdate(f);
                
                //Console.WriteLine(_progressBarVal);
            }
            catch (FileExistsException e)
            {
                Console.WriteLine(e);
                Environment.Exit(1);
            }
            catch (DirectoryExistsException e)
            {
                Console.WriteLine(e);
                Environment.Exit(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ProgressBarUpdate(Task f)
        {
            var formerPos = Console.GetCursorPosition();
            while (f.Status!=TaskStatus.RanToCompletion)
            {
                System.Threading.Thread.Sleep(100);
                Console.SetCursorPosition(0,formerPos.Top);
                Console.WriteLine(Math.Round(_progressBarVal*100,2) + "%                      ");
            }
            Console.WriteLine();
        }
    }
}
