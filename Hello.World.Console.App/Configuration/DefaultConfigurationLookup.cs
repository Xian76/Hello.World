using System;
using System.Configuration;
using Hello.World.ConsoleApp.Interfaces;

namespace Hello.World.ConsoleApp.Configuration
{
    /// <summary>
    /// <see cref="IConfigurationLookup"/> implementation which retrieves values from the App.Config or Web.Config files. 
    /// </summary>
    [Serializable]
    public sealed class DefaultConfigurationLookup : IConfigurationLookup
    {
        /// <summary>
        /// Singleton instance of <see cref="DefaultConfigurationLookup"/>. 
        /// </summary>
        public static readonly DefaultConfigurationLookup Default = new DefaultConfigurationLookup();

        /// <summary>
        /// Retrieves the value for the specified key.
        /// </summary>
        /// <param name="key">Key in the underlying configuration store.</param>
        /// <returns>A string representation of the value associated with the key, or null if there's no value for the specified key.</returns>
        public string this[string key] => ConfigurationManager.AppSettings[key];
    }
}