using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PwcBios.Api.Infrastructure.Filters
{
    public class PwcBiosExceptionFilter : IExceptionFilter
    {
        private readonly IPwcBiosExceptionHandler _handler;
        public PwcBiosExceptionFilter(IPwcBiosExceptionHandler handler)
        {
            _handler = handler;
        }

        public void OnException(ExceptionContext context)
        {
            var response = _handler.HandleException(context.Exception);

            context.ExceptionHandled = true;

            context.HttpContext.Response.StatusCode = (int)response.StatusCode;

            context.Result = new JsonResult(new { Message = response.Message });
        }
    }
}
