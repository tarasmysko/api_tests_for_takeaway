using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Test_Task.Common
{
    public class TimeMeasurementResponse : IRestResponse
    {
        public TimeMeasurementResponse(IRestResponse response, TimeSpan elapsedTime)
        {
            Response = response;
            ElapsedTime = elapsedTime;
        }

        private IRestResponse Response { get; }
        public TimeSpan ElapsedTime { get; }
        public double ElapsedMiliseconds => ElapsedTime.TotalMilliseconds;

        public IRestRequest Request { get => Response.Request; set => Response.Request = value; }
        public string ContentType { get => Response.ContentType; set => Response.ContentType = value; }
        public long ContentLength { get => Response.ContentLength; set => Response.ContentLength = value; }
        public string ContentEncoding { get => Response.ContentEncoding; set => Response.ContentEncoding = value; }
        public string Content { get => Response.Content; set => Response.Content = value; }
        public HttpStatusCode StatusCode { get => Response.StatusCode; set => Response.StatusCode = value; }

        public bool IsSuccessful => Response.IsSuccessful;

        public string StatusDescription { get => Response.StatusDescription; set => Response.StatusDescription = value; }
        public byte[] RawBytes { get => Response.RawBytes; set => Response.RawBytes = value; }
        public Uri ResponseUri { get => Response.ResponseUri; set => Response.ResponseUri = value; }
        public string Server { get => Response.Server; set => Response.Server = value; }

        public IList<RestResponseCookie> Cookies => Response.Cookies;

        public IList<Parameter> Headers => Response.Headers;

        public ResponseStatus ResponseStatus { get => Response.ResponseStatus; set => Response.ResponseStatus = value; }
        public string ErrorMessage { get => Response.ErrorMessage; set => Response.ErrorMessage = value; }
        public Exception ErrorException { get => Response.ErrorException; set => Response.ErrorException = value; }
        public Version ProtocolVersion { get => Response.ProtocolVersion; set => Response.ProtocolVersion = value; }
    }
}
