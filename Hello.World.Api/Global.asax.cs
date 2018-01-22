using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Hello.World.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Sets the default serialization settings
            var serializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            var contractResolver = (DefaultContractResolver)serializerSettings.ContractResolver;
            contractResolver.IgnoreSerializableAttribute = true;

            //Registers the WebApi routes
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Registers the unity components
            UnityConfig.RegisterComponents();
        }
    }
}
