using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.GlobalExceptionResponse
{
    public class ErrorMessage : IErrorMessage
    {
        public ErrorDetail Error(string? message)
        {
            return new ErrorDetail
            {
                Success = false,
                Message = message,
                Code = 404
            };
        }
        public ErrorDetail Error(string? message, int code)
        {
            return new ErrorDetail
            {
                Success = false,
                Message = message,
                Code = code
            };
        }
        public ErrorDetail Error(string? message, int code, List<string> errors)
        {
            return new ErrorDetail
            {
                Success = false,
                Message = message,
                Code = code,
                Errors = errors
            };
        }

        public ErrorDetail Success(string? message)
        {
            return new ErrorDetail
            {
                Message = message
            };
        }
        public ErrorDetail Success(string? message, int code)
        {
            return new ErrorDetail
            {
                Message = message,
                Code = code,
            };
        }
    }
}
