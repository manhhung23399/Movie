using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Core.Resources.Response
{
    public class ResponseBase
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public object errors { get; set; } = new object();
        public static ResponseBase Success()
        {
            return new ResponseBase
            {
                code = 200,
                data = new List<string>(),
                success = true,
                message = ""
            };
        }
        public static ResponseBase Success(object data)
        {
            return new ResponseBase
            {
                code = 200,
                data = data,
                success = true,
                message = ""
            };
        }

        public static ResponseBase Success(string message)
        {
            return new ResponseBase
            {
                code = 200,
                message = message,
                data = new List<object>(),
                success = true
            };
        }
        public static ResponseBase Success(string message, int code)
        {
            return new ResponseBase
            {
                code = code,
                message = message,
                data = new List<object>(),
                success = true
            };
        }
        public static ResponseBase Success(string message, int code, object data)
        {
            return new ResponseBase
            {
                code = code,
                message = message,
                data = data,
                success = true
            };
        }
        public static ResponseBase Error(string message, int code, object errors)
        {
            return new ResponseBase
            {
                code = code,
                message = message,
                success = false
            };
        }
    }
}
