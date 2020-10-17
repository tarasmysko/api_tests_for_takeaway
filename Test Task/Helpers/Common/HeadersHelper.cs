using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Test_Task.Common
{
    public class HeadersHelper
    {
        public static Parameter HeadersList(IRestResponse response, string search)
        {
            return response.Headers.FirstOrDefault(s => s.Name.ToLower() == search.ToLower());
        }
    }
}
