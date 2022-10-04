using RabbitMQ.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Core.Entities
{
    [BsonCollection("user")]
    public class User : Document
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
