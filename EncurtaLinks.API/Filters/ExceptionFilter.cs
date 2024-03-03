using EncurtaLinks.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EncurtaLinks.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new
            {
                Error = true,
                Message = context.Exception.Message
            };

            if(context.Exception is CustomException customException)
            {

                context.Result = new JsonResult(response)
                {
                    StatusCode = customException.StatusCode
                };
            }
            else
            {
                context.Result = new JsonResult(response)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
