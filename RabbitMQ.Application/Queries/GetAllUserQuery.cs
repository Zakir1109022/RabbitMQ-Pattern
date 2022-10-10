using MediatR;
using RabbitMQ.Application.Responses;
using RabbitMQ.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Application.Queries
{
   public class GetAllUserQuery : IRequest<Response<UserDto>>
    {

    }
}
