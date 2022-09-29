using AutoMapper;
using RabbitMQ.Application.Commands;
using RabbitMQ.Common.Events;
using RabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderCheckoutEvent, CreateOrderCommand>().ReverseMap();
            CreateMap<OrderCheckoutEvent, SentEmailCommand>().ReverseMap();
        }
    }
}
