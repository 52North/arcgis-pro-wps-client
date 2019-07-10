using System;
using System.IO;

namespace AgpWps.Model.Services
{
    public class FileReader : IFileReader
    {
        public string Read(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));

            return File.ReadAllText(path);
        }
    }
}
