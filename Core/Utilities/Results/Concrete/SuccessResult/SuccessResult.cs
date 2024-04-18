using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete.SuccessResult
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message, HttpStatusCode statusCode) : base(true, message, statusCode)
        {
        }
        public SuccessResult(HttpStatusCode statusCode) : base(true, statusCode)
        {
        }
    }
}
