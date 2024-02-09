using Microsoft.AspNetCore.Http;

namespace WooneasyManagement.Application.Common;

public sealed record Error(string Type, string Detail, int StatusCode)
{
    public static readonly Error None = new(string.Empty, string.Empty, StatusCodes.Status400BadRequest);
}
