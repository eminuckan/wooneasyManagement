using Microsoft.AspNetCore.Mvc;
using WooneasyManagement.Application.Common.Exceptions;

namespace WooneasyManagement.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Exception ocurred: {ex.Message}");
            var exceptionDetails = GetExceptionDetails(ex);

            var problemDetails = new ProblemDetails
            {
                Status = exceptionDetails.Status,
                Detail = exceptionDetails.Detail,
                Type = exceptionDetails.Type
            };

            if (exceptionDetails.Errors is not null) problemDetails.Extensions["errors"] = exceptionDetails.Errors;

            context.Response.StatusCode = exceptionDetails.Status;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception ex)
    {
        return ex switch
        {
            //if the exception is a ValidationException
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "One or more validation errors has occurred",
                validationException.Errors
            ),
            BadHttpRequestException badHttpRequestException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "BadRequest",
                "Bad request",
                null
            ),
            // default: other exceptions
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "An unexpected error has occurred server side",
                null
            )
        };
    }

    internal record ExceptionDetails(
        int Status,
        string Type,
        string Detail,
        IEnumerable<object>? Errors
    );
}