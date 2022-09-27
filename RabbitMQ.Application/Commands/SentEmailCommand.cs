using MediatR;
using RabbitMQ.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Application.Commands
{
   public class SentEmailCommand : IRequest<OrderResponse>
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}
