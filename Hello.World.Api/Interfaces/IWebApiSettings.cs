namespace Hello.World.Api.Interfaces
{
    /// <summary>
    /// Setting for the clients of the Web API.
    /// </summary>
    public interface IWebApiSettings
    {
        /// <summary>
        /// The location of the data file holding the returned data
        /// </summary>
        string DataFileLocation { get; }
    }
}