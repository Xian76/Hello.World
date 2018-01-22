using Hello.World.Api.Models;

namespace Hello.World.Api.Tests.UnitTests.Shared
{
   public class CommonTestMethods
    {
        #region Helper Methods
        /// <summary>
        ///     Gets a sample HelloWorldData Message string result
        /// </summary>
        /// <returns>A sample HelloWorldData model Message string</returns>
        protected static string GetSampleHelloWorldDataAsString() => new HelloWorldData("Hello World").Message;

        /// <summary>
        ///     Gets a sample HelloWorldData model
        /// </summary>
        /// <returns>A sample HelloWorldData model</returns>
        protected static HelloWorldData GetSampleHelloWorldData() => new HelloWorldData("Hello World");
        #endregion
    }
}
