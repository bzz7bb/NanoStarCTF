using Newtonsoft.Json.Linq;
using System;
namespace NanoStarCTF.Service.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public int Code { get; set; }

        public HttpStatusCodeException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
