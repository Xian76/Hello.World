using System;
using Hello.World.ConsoleApp.Configuration;
using Hello.World.ConsoleApp.Interfaces;
using Hello.World.ConsoleApp.Services;
using Unity;

namespace Hello.World.ConsoleApp.Application
{
    public class Program
    {
        /// <summary>
        /// Console app's initial method, which registers all of our Unity objects for dependency injection
        /// </summary>
        /// <param name="args">String array passed in, currently ignored</param>
       public static void Main(string[] args)
        {
            try
            {
                var container = new UnityContainer();
                container.RegisterType<IConsoleAppSettings, GlobalSettings>()
                    .RegisterType<IGetDataService, GetWebApiDataService>()
                    .RegisterType<IGetDataConsoleApplication, GetDataConsoleApplication>()
                    ;

                //Get a reference to the main program
                var program = container.Resolve<GetDataConsoleApplication>();

                //Call the run method on the reference to start the console application
                program.Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured : {ex}");
            }

        }
    }
}
