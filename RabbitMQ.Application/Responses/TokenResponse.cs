using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Application.Responses
{
   public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
