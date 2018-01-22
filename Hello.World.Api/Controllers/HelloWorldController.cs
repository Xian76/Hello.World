using System;
using System.Web.Http;
using Hello.World.Api.Interfaces;
using Hello.World.Api.Models;

namespace Hello.World.Api.Controllers
{
    /// <summary>
    /// API Controller for getting data to display
    /// </summary>
    public class HelloWorldController : ApiController
    {
        /// <summary>
        /// The data service for the controller
        /// </summary>
        private readonly IDataService dataService;

        /// <summary>
        /// Initializes a new intance of the <see cref="HelloWorldController"/> class
        /// </summary>
        /// <param name="dataService">The unity injected data service</param>
        public HelloWorldController(IDataService dataService)
        {
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        /// <summary>
        /// Get the string data returned by the data service
        /// </summary>
        /// <returns>A string representing the data</returns>
        [Route("api/HelloWorld/GetData")]
        public string GetData()
        {
            HelloWorldData helloWorldData;

            try
            {
                helloWorldData = dataService.GetData();
            }
            catch (ArgumentNullException argumentNullException)
            {
                //Todo Add error logging
                Console.WriteLine(argumentNullException);
                return "";
            }

            //ToDo need to decide how to handle system errors and what should be returned
            return string.IsNullOrEmpty(helloWorldData?.Message) ? "" : helloWorldData.Message;
        }
    }
}