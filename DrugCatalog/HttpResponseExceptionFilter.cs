using DrugCatalog.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DrugCatalog
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BusinessException exception)
            {
                int statusCode = (int)HttpStatusCode.BadRequest;
                if(context.Exception is NotFoundException)
                {
                    statusCode = (int)HttpStatusCode.NotFound;
                }

                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = statusCode,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
