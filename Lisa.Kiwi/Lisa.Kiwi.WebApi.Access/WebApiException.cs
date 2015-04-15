using System;
using System.Net;

namespace Lisa.Kiwi.WebApi.Access
{
    public class WebApiException : Exception
    {
        public WebApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public WebApiException(string message, HttpStatusCode statusCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
