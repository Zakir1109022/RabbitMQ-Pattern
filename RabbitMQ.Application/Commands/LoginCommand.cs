using MediatR;
using RabbitMQ.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Application.Commands
{
   public class LoginCommand : IRequest<TokenResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
