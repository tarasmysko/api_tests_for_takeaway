using Microsoft.Extensions.Configuration;
using System;

namespace Test_Task.Common
{
    public class ConfigHelper
    {
        public static string BaseUrl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["BaseURL"];
        public static int Repetitions = Int32.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["Repeat"]);
    }
}
