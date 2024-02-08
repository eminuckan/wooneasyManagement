using Microsoft.AspNetCore.Http;

namespace WooneasyManagement.Application.Common
{
    public class Result
    {

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public object? Data { get; set; }

        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;

        }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);
    }

    public sealed record Error(string Type, string Detail, int StatusCode)
    {
        public static readonly Error None = new Error(string.Empty, string.Empty, StatusCodes.Status400BadRequest);
    }
}
