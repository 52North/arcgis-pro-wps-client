namespace AgpWps.Model.Services
{
    public interface IFileReader
    {

        /// <summary>
        /// Read all text from a file.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>The text contained by the file</returns>
        string Read(string path);

    }
}
