namespace WooneasyManagement.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyCollection<ValidationError> Errors { get; }

        public ValidationException(IReadOnlyCollection<ValidationError> errors) : base("Validation exception occurred")
        {
            Errors = errors;
        }
    }
}

public record ValidationError(string PropertyName, string ErrorMessage);