using Unity;

namespace Hello.World.Api.Interfaces
{
    /// <summary>
    /// Interface which encapsulates any class which knows how to configure a Unity IOC container.
    /// </summary>
    public interface IContainerBootstrapper
    {
        /// <summary>
        /// Configures Unity IOC container registrations.
        /// </summary>
        /// <param name="container">The Unity container to configure.</param>
        void RegisterTypes(IUnityContainer container);
    }
}