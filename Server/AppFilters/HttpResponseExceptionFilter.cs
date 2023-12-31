﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Server.AppFilters;



public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    private readonly ILogger<HttpResponseExceptionFilter> logger;

    public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
    {
        this.logger = logger;
    }

    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context?.Exception is not null && context.Exception is Exception exception)
        {
            logger.LogError(
@$"[{context?.HttpContext?.Request?.Method}] {context?.HttpContext?.Request?.Path}  
ExceptionType: {exception.GetType().Name}, 
Message: {exception.Message}. 
Exception: {{@exception}}", exception);


            context.Result = new ObjectResult(Result.Exception<string>(exception.GetType().Name, exception.Message))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}

