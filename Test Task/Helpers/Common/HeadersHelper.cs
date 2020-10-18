using RestSharp;
using System.Linq;

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
