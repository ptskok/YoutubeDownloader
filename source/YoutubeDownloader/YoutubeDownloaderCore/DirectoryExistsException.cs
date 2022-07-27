using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeDownloaderCore
{
    public class DirectoryExistsException : Exception
    {
        public DirectoryExistsException()
        {
        }

        public DirectoryExistsException(string message)
            : base(message)
        {
        }

        public DirectoryExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
