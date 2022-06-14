using System;

namespace YoutubeDownloaderCore
{
    public class Downloader
    {
        private readonly string _saveVideoPath;
        private readonly string _videoLink;
        public Downloader(string saveVideoPath, string videoLink)
        {
            _saveVideoPath = saveVideoPath;
            _videoLink = videoLink;
        }

        public void Download(int option)
        {
            switch (option)
            {
                case 'a':
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

        private void DownloadAudio()
        {
            throw new NotImplementedException("This method is not yet implemented!");
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
