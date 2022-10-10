using MediatR;
using RabbitMQ.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Application.Commands
{
   public class GenerateAccessTokenCommand : IRequest<TokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
