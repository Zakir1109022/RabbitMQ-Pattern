using MediatR;
using RabbitMQ.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Application.Queries
{
    public class GetAllOrderQuery : IRequest<OrderResponse>
    {
      
    }
}
