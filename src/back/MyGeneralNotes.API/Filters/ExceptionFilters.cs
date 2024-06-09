using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyGeneralNotes.Communication.Responses;
using MyGeneralNotes.Exceptions;
using MyGeneralNotes.Exceptions.ExceptionsBase;
using System.Net;

namespace MyGeneralNotes.API.Filters;

public class ExceptionFilters : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MyGeneralNotesExceptions)
            HandleProjectException(context);
        else
        {
            ThrowUnknowException(context);
        }
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is InvalidLoginException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new UnauthorizedObjectResult(new ResponseError(context.Exception.Message));
        }
        else if (context.Exception is ErrorOnValidationException)
        {
            var exception = context.Exception as ErrorOnValidationException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseError(exception!.ErrorsMessages));
        }
    }

    private static void ThrowUnknowException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new BadRequestObjectResult(new ResponseError(MessagesException.UNKNOWN_ERROR));
        }
    }
}
