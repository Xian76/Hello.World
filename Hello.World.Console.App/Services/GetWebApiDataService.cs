using System;
using System.Configuration;
using System.Net.Http;
using Hello.World.ConsoleApp.Interfaces;

namespace Hello.World.ConsoleApp.Services
{
    /// <summary>
    /// Service for communicating with the Hello World API
    /// </summary>
    public class GetWebApiDataService : IGetDataService
    {
        /// <summary>
        /// Helllo World console application settings
        /// </summary>
        private readonly IConsoleAppSettings _consoleAppSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetWebApiDataService"/>
        /// </summary>
        /// <param name="consoleAppSettings">Injected console application settings</param>
        public GetWebApiDataService(IConsoleAppSettings consoleAppSettings)
        {
            _consoleAppSettings = consoleAppSettings ?? throw new ArgumentNullException(nameof(consoleAppSettings));
        }

        /// <summary>
        /// Gets the data from the Web API
        /// </summary>
        /// <returns>Data to be displayed</returns>
        public string GetData()
        {
            //Throws an error if the console settings file does not have a valid entry for the Web API endpoint
            if (string.IsNullOrEmpty(_consoleAppSettings.WebApiLocation))
                throw new SettingsPropertyNotFoundException($"DataFileLocation");

            //Get the Web API endpont from the settings file and append the controller and action
            var url = $"{_consoleAppSettings.WebApiLocation}HelloWorld/GetData";
            string result;

            //Use an instance of the HttpClient to initiate a connection with the Web API
            using (var client = new HttpClient())
            {
                //Forces the async method to run synchronously as we cannot use async await from the Run method of the console application
                // ReSharper disable once AccessToDisposedClosure
                result = AsyncHelpers.RunSync(() => client.GetStringAsync(url));
                //Strip out the escaped quotation marks
                result = result?.Replace("\"", "");
            }
            return result;
        }
    }
}
