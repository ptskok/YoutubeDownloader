using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloaderCore
{
    public class Downloader
    {
        private readonly string _saveVideoPath;
        private readonly string _videoLink;
        private readonly string ffMpegPath = "C:\\Users\\ptsko\\OneDrive\\Počítač\\ffmpeg";

        public Downloader(string saveVideoPath, string videoLink)
        {
            _saveVideoPath = saveVideoPath;
            _videoLink = videoLink;
        }

        public async Task Download(int option)
        {
            switch (option)
            {
                case 'a':
                    await DownloadAudio();
                    break;
                case 'b':
                    break;
                case 'c':
                    break;
                case 'd':
                    break;
                default:
                    throw new Exception("No such option implemented!");
            }
        }

        private async Task DownloadAudio()
        {
            var savePath = Path.GetFullPath(_saveVideoPath);
            var id = VideoId.Parse(_videoLink);
            Console.WriteLine(id.Value);
            var youtube = new YoutubeClient();

            var fp = $"{savePath}\\videoDone.mp3";
            Console.WriteLine("saving as: {0}",fp);
            try
            {
                await youtube.Videos.DownloadAsync(id, fp, o => o
                    .SetContainer("webm")
                    .SetPreset(ConversionPreset.Medium)
                    .SetFFmpegPath(ffMpegPath)
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Console.WriteLine("Done!");
        }

        private void DownloadVideo()
        {
            throw new NotImplementedException("This method is not yet implemented!");
        }

        private void DownloadAudioPlaylist()
        {
            throw new NotImplementedException("This method is not yet implemented!");
        }

        private void DownloadVideoPlaylist()
        {
            throw new NotImplementedException("This method is not yet implemented!");
        }
    }
}
