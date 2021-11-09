using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Infrastructure.GlobalExceptionResponse
{
    public class ErrorDetail
    {
        public int Code { get; set; } = 200;
        public bool? Success { get; set; } = true;
        public string? Message { get; set; }
        public List<string> Errors {  get; set; } = new List<string>();
    }
}
