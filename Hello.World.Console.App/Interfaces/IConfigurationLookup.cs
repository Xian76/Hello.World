namespace Hello.World.ConsoleApp.Interfaces
{
    /// <summary>
    /// Retrieves values from an underlying configuration store based on keys.
    /// </summary>
    public interface IConfigurationLookup
    {
        // Note: All implementations of this interface should return null if a value was not found for the key.
        /// <summary>
        /// Retrieves the value for the specified key.
        /// </summary>
        /// <param name="key">Key in the underlying configuration store.</param>
        /// <returns>A string representation of the value associated with the key, or null if there's no value for the specified key.</returns>
        string this[string key] { get; }
    }
}