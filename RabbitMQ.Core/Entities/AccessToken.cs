﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Core.Entities
{
   public class AccessToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
