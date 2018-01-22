using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Hello.World.Api
{
    /// <summary>
    /// Configure dependency injection using Unity
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Register the applications components
        /// </summary>
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            var bootstrapper = new ContainerBootstrapper();
            bootstrapper.RegisterTypes(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}