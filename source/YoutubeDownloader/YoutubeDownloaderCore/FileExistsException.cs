using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloaderCore
{
    public class FileExistsException : Exception
    {
        public FileExistsException()
        {
        }

        public FileExistsException(string message)
            : base(message)
        {
        }

        public FileExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
