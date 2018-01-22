using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello.World.Api.Interfaces
{
    public interface IFileReader
    {
        string ReadAllText(string filePath);
    }
}