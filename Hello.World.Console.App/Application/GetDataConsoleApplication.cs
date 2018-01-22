using System;
using Hello.World.ConsoleApp.Interfaces;

namespace Hello.World.ConsoleApp.Application
{
    /// <summary>
    /// Hello World console application
    /// </summary>
    public class GetDataConsoleApplication : IGetDataConsoleApplication
    {
        /// <summary>
        /// Hello World get data service
        /// </summary>
        private readonly IGetDataService _getDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDataConsoleApplication"/>
        /// </summary>
        /// <param name="getDataService">Injected hello world data service</param>
        public GetDataConsoleApplication(IGetDataService getDataService)
        {
            _getDataService = getDataService ?? throw new ArgumentNullException(nameof(getDataService));
        }

        /// <summary>
        /// Runs the Hello World console application
        /// </summary>
        /// <param name="arguments">The command line arguments.</param>
        public void Run(string[] arguments)
        {
            //Uses the Hello World registered data service to get the data
            var webApiData = _getDataService.GetData();

            //Writes the returned data to the console
            Console.WriteLine(webApiData ?? "No data found");
        }
    }
}
