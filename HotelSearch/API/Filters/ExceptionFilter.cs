using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var errorResponse = new
        {
            Message = "An unexpected error occurred.",
            Details = context.Exception.Message
        };
        context.Result = new JsonResult(errorResponse) { StatusCode = 500 };
        context.ExceptionHandled = true;
    }
}
