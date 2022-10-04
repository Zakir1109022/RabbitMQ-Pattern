using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Core.Dtos
{
   public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; }
    }
}
