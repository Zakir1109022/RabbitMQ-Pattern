using MediatR;
using RabbitMQ.Application.Responses;
using RabbitMQ.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Application.Commands
{
   public class CreateUserCommand : IRequest<Response<UserDto>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
