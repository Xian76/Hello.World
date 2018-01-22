namespace Hello.World.ConsoleApp.Interfaces
{/// <summary>
    /// Setting for the clients of the console application.
    /// </summary>
    public interface IConsoleAppSettings
    {
        /// <summary>
        /// Uri to the API endpoint for retrieving the data to be displayed in the console application
        /// </summary>
        string WebApiLocation { get; }
    }
}
