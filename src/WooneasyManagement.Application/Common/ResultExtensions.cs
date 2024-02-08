using Microsoft.AspNetCore.Http;

namespace WooneasyManagement.Application.Common
{
    public static class ResultExtensions
    {
        public static T Match<T>(
            this Result result,
            Func<T> onSuccess,
            Func<Error, T> onFailure
        )
        {
            return result.IsSuccess ? onSuccess() : onFailure(result.Error);
        }

        public static Result WithData(this Result result, object? data)
        {
            if (result.IsFailure)
            {
                throw new InvalidOperationException("WithData can only be used on successful results.");
            }
            result.Data = data;
            return result;
        }

        public static IResult ToProblemDetails(this Result result)
        {

            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Cannot create problem details for a successful result");
            }

            return Results.Problem(
                  detail: result.Error.Detail,
                  type: result.Error.Type,
                  statusCode: result.Error.StatusCode
            );
        }

  
    }
}
