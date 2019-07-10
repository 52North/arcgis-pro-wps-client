using AgpWps.Model.Services;
using System.IO;

namespace AgpWps.Model.Tests
{
    public class FileReaderMock : IFileReader
    {

        public const string CvmTestPath = "cvm_test_path";
        public const string CvmTestPathResult = "this is a string";

        public string Read(string path)
        {
            if (path.Equals(CvmTestPath))
            {
                return CvmTestPathResult;
            }
            else
            {
                return File.ReadAllText(path);
            }
        }
    }
}
