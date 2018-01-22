using Hello.World.Api.Configuration;
using Hello.World.Api.Controllers;
using Hello.World.Api.Interfaces;
using Hello.World.Api.Services;
using Unity;

namespace Hello.World.Api
{
    /// <summary>
    /// Unity container register
    /// </summary>
    public class ContainerBootstrapper : IContainerBootstrapper
    {
        /// <summary>
        /// Register the classes used for dependency injection
        /// </summary>
        /// <param name="container"></param>
        public void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<IWebApiSettings, GlobalSettings>()
                .RegisterType<IDataService, TextFileService>()
                .RegisterType<IFileReader, FileReaderService>()
                .RegisterType<HelloWorldController>();
        }
    }
}