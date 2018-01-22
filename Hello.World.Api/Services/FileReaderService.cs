using Hello.World.Api.Interfaces;

namespace Hello.World.Api.Services
{
    public class FileReaderService : IFileReader
    {
        /// <summary>
        /// The location of the applications bin folder
        /// </summary>
        private string BinFolderPath => System.IO.Path
            .GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
            ?.Substring(6);

        /// <summary>
        /// Method to return the text found in the passed in fle path
        /// </summary>
        /// <param name="filePath">Location of the text file to be read</param>
        /// <returns>The contents of the text file</returns>
        public string ReadAllText(string filePath) => System.IO.File.ReadAllText($"{BinFolderPath}{filePath}");
    }
}