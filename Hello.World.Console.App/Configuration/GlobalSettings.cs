using Hello.World.ConsoleApp.Interfaces;

namespace Hello.World.ConsoleApp.Configuration
{
    public class GlobalSettings : SmartSettingsBase, IConsoleAppSettings
    {
        #region IConsoleAppSettings (Data API)
        public string WebApiLocation => GetValueWithCaching<IConsoleAppSettings, string>(s => s.WebApiLocation);
        #endregion
    }
}