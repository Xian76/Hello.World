using Hello.World.Api.Models;

namespace Hello.World.Api.Interfaces
{
    /// <summary>
    /// Service for data interaction
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Get data to be displayed
        /// </summary>
        /// <returns>HelloWorldData model containing the message to be returned</returns>
        HelloWorldData GetData();
    }
}