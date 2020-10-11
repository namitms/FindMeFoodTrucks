using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindMeFoodTrucks.WebAPI.Helpers
{
    public class HttpResponseException : Exception
    {
        public string Message;
        public int Status { get; set; } = 400;
        public object Value { get; set; }
    }
}
