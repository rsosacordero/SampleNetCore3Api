using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PwcBios.Api.Infrastructure.Exceptions
{
    public class ApiError
    {
        public string Message { get; set; }
        public string Detail { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiError(string message, string detail, HttpStatusCode statusCode)
        {
            Message = message;
            Detail = detail;
            StatusCode = statusCode;
        }
    }
}
