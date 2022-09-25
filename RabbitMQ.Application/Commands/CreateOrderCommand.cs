using MediatR;
using RabbitMQ.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Application.Commands
{
    public class CreateOrderCommand: IRequest<OrderResponse>
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string EmailAddress { get; set; }
    }
}
