using NUnit.Framework.Internal;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task.Common
{
    public abstract class Api
    {
        private readonly string _serviceUrl;
        private readonly RestClient _client;
        protected readonly ILogger _logger;

        protected Api(string serviceUrl)
        {
            _serviceUrl = serviceUrl;
            _client = new RestClient(UrlHelper.BaseUrl);
        }

        protected RestRequest CreateGetRequest(string url = null)
        {
            return CreateRequestWithServiceUrl(url, Method.GET);
        }

        private RestRequest CreateRequestWithServiceUrl(string url, Method methodType)
        {
            return CreateRequest(CreateServiceUrl(url), methodType);
        }

        private RestRequest CreateRequest(string url, Method methodType)
        {
            var restRequest = new RestRequest
            {
                Resource = url,
                RequestFormat = DataFormat.Json,
                Method = methodType
            };

            restRequest.AddHeader("Content-type", "application/json");

            return restRequest;
        }

        private string CreateServiceUrl(string url)
        {
            if (_serviceUrl == null)
            {
                return url;
            }

            return url == null
                ? _serviceUrl
                : $"{_serviceUrl}/{url}";
        }

        protected async Task<TimeMeasurementResponse<TResponse>> ExecuteRequest<TResponse>(IRestRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await _client.ExecuteAsync<TResponse>(request);
            stopwatch.Stop();

            return new TimeMeasurementResponse<TResponse>(response, stopwatch.Elapsed);
        }

        //protected async Task<TimedRestResponse<IRestResponse<TResponse>>> ExecuteRequest<TResponse>(IRestRequest request)
        //{
        //    return await TimedRestResponse<IRestResponse<TResponse>>.Measure(async () => await _client.ExecuteAsync<TResponse>(request));
        //    //return _client.ExecuteAsync<TResponse>(request);
        //}
    }
}
