namespace Hello.World.ConsoleApp.Interfaces
{
    /// <summary>
    /// Service for communicating with the Hello World API
    /// </summary>
    public interface IGetDataService
    {
        /// <summary>
        /// Gets the data from the Web API
        /// </summary>
        /// <returns>Data to be displayed</returns>
        string GetData();
    }
}
