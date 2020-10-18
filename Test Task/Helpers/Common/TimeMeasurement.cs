using RestSharp;
using System;

namespace Test_Task.Common
{
    public class TimeMeasurementResponse<T> : TimeMeasurementResponse, IRestResponse<T>
    {
        private IRestResponse<T> Response { get; }

        public TimeMeasurementResponse(IRestResponse<T> response, TimeSpan elapsedTime)
            : base(response, elapsedTime)
        {
            Response = response;
        }

        public T Data { get => Response.Data; set => Response.Data = value; }
    }
}
