using RabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Application.Responses
{
    public class OrderResponse
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
