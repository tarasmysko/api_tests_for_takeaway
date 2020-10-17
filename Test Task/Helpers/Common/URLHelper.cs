using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Test_Task.Common
{
    public class UrlHelper
    {
        public static string BaseUrl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["BaseURL"];
    }
}
