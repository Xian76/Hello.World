using System;
using System.Configuration;
using Hello.World.Api.Interfaces;
using Hello.World.Api.Models;

namespace Hello.World.Api.Services
{
    /// <summary>
    /// Service class used to retrieve data from the file defined in the web.config
    /// </summary>
    public class TextFileService : IDataService
    {
        /// <summary>
        /// The web application settings returned from the web.config
        /// </summary>
        private readonly IWebApiSettings _webApiSettings;
        /// <summary>
        /// Service reference for the file reading service
        /// </summary>
        private readonly IFileReader _fileReaderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFileService"/> class.
        /// </summary>
        /// <param name="webApiSettings">The web application settings</param>
        /// <param name="fileReaderService">The file reading service</param>
        public TextFileService(IWebApiSettings webApiSettings, IFileReader fileReaderService)
        {
            _webApiSettings = webApiSettings ?? throw new ArgumentNullException(nameof(webApiSettings));
            _fileReaderService = fileReaderService ?? throw new ArgumentNullException(nameof(fileReaderService));
        }

        /// <summary>
        /// Retrieves the data from the file designated by the web.config setting 
        /// </summary>
        /// <returns>String data from the file</returns>
        public HelloWorldData GetData()
        {
            //Verify the setting contain an entry for the file location
            if (string.IsNullOrEmpty(_webApiSettings.DataFileLocation))
                throw new SettingsPropertyNotFoundException($"DataFileLocation");

            //Uses the file reading service to get the data from the file
            var textFromFile = _fileReaderService.ReadAllText($"{_webApiSettings.DataFileLocation}");

            //Verify the file has data 
            if (string.IsNullOrEmpty(textFromFile))
                throw new ArgumentNullException($"DataFileLocation file was empty");

            return new HelloWorldData(textFromFile);
        }
    }
}