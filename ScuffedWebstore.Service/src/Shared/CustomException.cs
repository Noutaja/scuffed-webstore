using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScuffedWebstore.Service.src.Shared
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }

        public CustomException(int statusCode, string msg) : base(msg)
        {
            StatusCode = statusCode;
        }

        public static CustomException NotFoundException(string msg = "Not found")
        {
            return new CustomException(404, msg);
        }

        public static CustomException InvalidPasswordOrEmail(string msg = "Email or Password does not match")
        {
            return new CustomException(401, msg);
        }

        public static CustomException InvalidParameters(string msg = "Invalid parameters")
        {
            return new CustomException(422, msg);
        }
    }
}