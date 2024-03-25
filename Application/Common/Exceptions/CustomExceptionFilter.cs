using Core.Users.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Common.Exceptions;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CustomException)
        {
            var errorDetails = new ExceptionDetails(400, "BAD REQUEST", "Something goes wrong...");
            context.Result = new ObjectResult(errorDetails)
            {
                StatusCode = 400
            };
            context.ExceptionHandled = true;
        }
    }
}