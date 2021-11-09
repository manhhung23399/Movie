using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.GlobalExceptionResponse
{
    public interface IErrorMessage
    {
        ErrorDetail Success(string? message);
        ErrorDetail Success(string? message, int code);
        ErrorDetail Error(string? message);
        ErrorDetail Error(string? message, int code);
        ErrorDetail Error(string? message, int code, List<string> errors);
    }
}
