using RestSharp;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Test_Task.Common
{
    public abstract class Api
    {
        private readonly string _serviceUrl;
        private readonly RestClient _client;

        protected Api(string serviceUrl)
        {
            _serviceUrl = serviceUrl;
            _client = new RestClient(ConfigHelper.BaseUrl);
        }

        protected RestRequest CreateGetRequest(string url = null)
        {
            return CreateRequest(url, Method.GET);
        }

        protected RestRequest CreatePostRequest<TPayload>(TPayload payload)
        {
            var restRequest = CreateRequest(null, Method.POST);

            if (payload != null)
            {
                restRequest.AddJsonBody(payload);
            }

            return restRequest;
        }

        private RestRequest CreateRequest(string url, Method methodType)
        {
            var fullUrl = CreateServiceUrl(url);
            var restRequest = new RestRequest
            {
                Resource = fullUrl,
                RequestFormat = DataFormat.Json,
                Method = methodType
            };

            restRequest.AddHeader("Content-type", "application/json");

            return restRequest;
        }

        private string CreateServiceUrl(string url)
        {
            return url == null ? _serviceUrl : $"{_serviceUrl}/{url}";
        }

        protected async Task<TimeMeasurementResponse<TResponse>> ExecuteRequest<TResponse>(IRestRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await _client.ExecuteAsync<TResponse>(request);
            stopwatch.Stop();

            return new TimeMeasurementResponse<TResponse>(response, stopwatch.Elapsed);
        }
    }
}
