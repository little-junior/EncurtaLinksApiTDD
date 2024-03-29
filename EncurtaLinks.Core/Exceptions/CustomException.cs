using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncurtaLinks.Core.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public string Error { get; }

        public CustomException(string message, string error, int statusCode) : base(message)
        {
            Error = error;
            StatusCode = statusCode;
        }
    }
}
