using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Converter;
using YoutubeExplode.Playlists;
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
            var blockName = string.Empty;
            if (option == 'a' || option == 'b')
            {
                var youtube = new YoutubeClient();
                blockName = youtube.Videos.GetAsync(VideoId.Parse(_videoLink)).Result.Title;
                foreach (var file in Directory.GetFiles(_saveVideoPath))
                {
                    Console.WriteLine(file);
                }
            }

            switch (option)
            {

                case 'a':
                    if (Directory.GetFiles(_saveVideoPath).Contains(_saveVideoPath + "\\" + blockName + ".mp3"))
                    {
                        throw new FileExistsException("This track is already downloaded in the specified folder!");
                    }
                    await DownloadAudio();
                    break;
                case 'b':
                    if (Directory.GetFiles(_saveVideoPath).Contains(_saveVideoPath + "\\" + blockName + ".mp4"))
                    {
                        throw new FileExistsException("This track is already downloaded in the specified folder!");
                    }
                    await DownloadVideo();
                    break;
                case 'c':
                    await DownloadAudioPlaylist();
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

            var vid = youtube.Videos.GetAsync(id);

            var fp = $"{savePath}\\{vid.Result.Title}.mp3";
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

        private async Task DownloadVideo()
        {
            var savePath = Path.GetFullPath(_saveVideoPath);
            var id = VideoId.Parse(_videoLink);
            Console.WriteLine(id.Value);
            var youtube = new YoutubeClient();

            var vid = youtube.Videos.GetAsync(id);

            var fp = $"{savePath}\\{vid.Result.Title}.mp4";
            Console.WriteLine("saving as: {0}", fp);
            var streamManifest = youtube.Videos.Streams.GetManifestAsync(id.Value);
            var streamInfo = streamManifest.Result.GetMuxedStreams().GetWithHighestVideoQuality();

            await youtube.Videos.Streams.DownloadAsync(streamInfo, fp);
            Console.WriteLine("Done!");
        }

        private async Task DownloadVideoMuxing()
        {
            //Muxing streams (not yet finished)
            var savePath = Path.GetFullPath(_saveVideoPath);
            var id = VideoId.Parse(_videoLink);
            var youtube = new YoutubeClient();
            var vid = youtube.Videos.GetAsync(id);

            var fp = $"{savePath}\\{vid.Result.Title}.mp4";
            var streamManifest = youtube.Videos.Streams.GetManifestAsync(id.Value);

            var audioStreamInfo = streamManifest.Result.GetAudioStreams().GetWithHighestBitrate();
            var videoStreamInfo = streamManifest.Result.GetVideoStreams().GetWithHighestVideoQuality();
            var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };

            // Download and process them into one file
            await youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(fp)
                .SetPreset(ConversionPreset.UltraFast)
                .SetContainer("webm")
                .SetFFmpegPath(ffMpegPath)
                .Build()
            );
        }

        private async Task DownloadAudioPlaylist()
        {
            var savePath = Path.GetFullPath(_saveVideoPath);
            var youtube = new YoutubeClient();
            var playlistId = PlaylistId.Parse(_videoLink);
            var playlist = youtube.Playlists.GetAsync(playlistId);
            savePath += "\\" + playlist.Result.Title;
            Directory.CreateDirectory(savePath);
            await foreach (var video in youtube.Playlists.GetVideosAsync(playlistId.Value))
            {
                var vidSavePath = savePath + "\\" + video.Title + ".mp3";
                await youtube.Videos.DownloadAsync(video.Id, vidSavePath, o => o
                    .SetContainer("webm")
                    .SetPreset(ConversionPreset.Medium)
                    .SetFFmpegPath(ffMpegPath)
                );
            }
        }

        private void DownloadVideoPlaylist()
        {
            throw new NotImplementedException("This method is not yet implemented!");
        }
    }
}
