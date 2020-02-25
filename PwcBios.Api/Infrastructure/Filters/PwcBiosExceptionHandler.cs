using PwcBios.Api.Infrastructure.Exceptions;
using System;

namespace PwcBios.Api.Infrastructure.Filters
{
    public interface IPwcBiosExceptionHandler
    {
        ApiError HandleException(Exception exc);
    }
    public class PwcBiosExceptionHandler : IPwcBiosExceptionHandler
    {
        public ApiError HandleException(Exception exc)
        {
            ApiError errorResponse = null;
            //if (exc is ResourceNotFoundException)
            //{
            //    var rnfException = exc as ResourceNotFoundException;

            //    errorResponse = new ApiError(exc.Message, exc.InnerException?.StackTrace, System.Net.HttpStatusCode.NotFound);
            //}
            //else
            //{
                errorResponse = new ApiError("An unhandled error occurred.", null, System.Net.HttpStatusCode.InternalServerError);
            //}
            return errorResponse;
        }
    }
}
