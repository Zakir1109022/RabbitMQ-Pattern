using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Application.Responses
{
   public class Response<T> where T:class
    {
        public string Message { get; set; }
        public bool IsValid { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
