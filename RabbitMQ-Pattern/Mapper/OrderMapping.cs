using AutoMapper;
using RabbitMQ.Application.Commands;
using RabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Mapper
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}
