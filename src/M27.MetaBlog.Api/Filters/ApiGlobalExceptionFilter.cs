﻿using M27.MetaBlog.Application.Exceptions;
using M27.MetaBlog.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace M27.MetaBlog.Api.Filters;

public class ApiGlobalExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _env;
    public ApiGlobalExceptionFilter(IHostEnvironment env)
        => _env = env;

    public void OnException(ExceptionContext context)
    {
        var details = new ProblemDetails();
        var exception = context.Exception;

        if(exception is EntityValidationException entityValidationException)
        {
            details.Title = "One or more validation errors ocurred";
            details.Status = StatusCodes.Status422UnprocessableEntity;
            details.Type = "UnprocessableEntity";
            details.Extensions["errors"] = entityValidationException.Message.Split(" | ");
        }
        else if (exception is NotFoundException)
        {
            details.Title = "Not Found";
            details.Status = StatusCodes.Status404NotFound;
            details.Type = "NotFound";
            details.Detail = exception!.Message;
        }
        else if (exception is ConflictException)
        {
            details.Title = "Conflict";
            details.Status = StatusCodes.Status409Conflict;
            details.Type = "Conflict";
            details.Detail = exception!.Message;
        }
        else
        {
            details.Title = "An unexpected error ocurred";
            details.Status = StatusCodes.Status422UnprocessableEntity;
            details.Type = "UnexpectedError";
            details.Detail = exception.Message;
        }

        context.HttpContext.Response.StatusCode = (int) details.Status;
        context.Result = new ObjectResult(details);
        context.ExceptionHandled = true;
    }
}
