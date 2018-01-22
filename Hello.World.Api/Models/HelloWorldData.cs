using System;

namespace Hello.World.Api.Models
{
    /// <summary>
    /// Serializable model that contains the returned message
    /// </summary>
    [Serializable]
    public class HelloWorldData
    {
        public HelloWorldData(string message)
        {
            Message = message;
        }
        /// <summary>
        /// Message returned from server
        /// </summary>
        public string Message { get; set; }
    }
}