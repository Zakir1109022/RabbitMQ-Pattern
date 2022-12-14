using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Application.Services
{
   public  class PasswordHasherService: IPasswordHasherService
    {
        public PasswordHasherService()
        {

        }

        public string Hash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
            
        }

        public bool VerifyPassword(string passwordHash, string password)
        {
            bool verified = BCrypt.Net.BCrypt.Verify(password, passwordHash);
            return verified;
        }
    }
}
