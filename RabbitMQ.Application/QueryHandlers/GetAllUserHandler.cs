using AutoMapper;
using MediatR;
using RabbitMQ.Application.Queries;
using RabbitMQ.Application.Responses;
using RabbitMQ.Application.Services;
using RabbitMQ.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ.Application.QueryHandlers
{
   public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, Response<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<Response<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {

            var results = await _userService.GetAllAsync();

            var response = new Response<UserDto>()
            {
                Message="Success",
                IsValid=true,
                Results = _mapper.Map<IEnumerable<UserDto>>(results)
            };

            return response;
        }
    }
}
