using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WooneasyManagement.Application.Common
{
    public enum ErrorSeverity
    {
        Low,
        Medium,
        High
    }

    public record Error
    {
        public required string Title { get; set; }
        public string? Detail { get; set; }
        public int? StatusCode { get; set; }
        public string? Instance { get; set; }
        public ErrorSeverity? Severity { get; set; }

    }

    public class Result
    {
        public bool IsSuccess { get; set; }
        public Error? Error { get; set; }

        public static Result Ok()
        {
            return new Result { IsSuccess = true };
        }

        public static Result Fail(Error error)
        {
            return new Result { IsSuccess = false, Error = error };
        }

        public static Result Fail(string error)
        {
            return new Result { IsSuccess = false, Error = new Error { Title = error } };
        }
    }

    public class Result<T> : Result
    {
        public required T Value { get; set; }

        public static Result<T> Ok(T data)
        {
            return new Result<T> { IsSuccess = true, Value = data };
        }
    }

    public static class ResultExtensions
    {
        public static ProblemDetails ToProblemDetails(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Cannot create ProblemDetails for a successful result.");
            }
            return new()
            {
                Title = result.Error?.Title,
                Status = result.Error?.StatusCode,
                Detail = result.Error?.Detail,
                Instance = result.Error?.Instance,
                Type = result.Error?.GetType().Name,
                Extensions = { { "Severity", result.Error?.Severity } }
            };
        }

        public static IResult ToMinimalAPIResult(this Result result)
        {

            if (result.IsSuccess)
            {
                return Results.Ok();
            }

            var problemDetails = result.ToProblemDetails();

            return result.Error?.StatusCode switch
            {
                400 => Results.BadRequest(problemDetails),
                401 => Results.Unauthorized(),
                404 => Results.NotFound(problemDetails),
                403 => Results.Forbid(),
                409 => Results.Conflict(problemDetails),
                _ => Results.Problem(problemDetails)
            };
        }

        public static IResult ToMinimalAPIResult<T>(this Result<T> result , T data)
        {
            return Results.Ok(result.Value);
        }
    }


}
