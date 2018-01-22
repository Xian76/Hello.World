using Hello.World.Api.Interfaces;

namespace Hello.World.Api.Configuration
{
    public class GlobalSettings : SmartSettingsBase, IWebApiSettings
    {
        #region IWebApiSettings (Data API)
        public string DataFileLocation => GetValueWithCaching<IWebApiSettings, string>(s => s.DataFileLocation);
        #endregion
    }
}