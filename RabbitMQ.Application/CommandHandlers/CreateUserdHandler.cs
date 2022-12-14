using AutoMapper;
using MediatR;
using RabbitMQ.Application.Commands;
using RabbitMQ.Application.Responses;
using RabbitMQ.Application.Services;
using RabbitMQ.Core.Dtos;
using RabbitMQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ.Application.CommandHandlers
{
   public class CreateUserdHandler : IRequestHandler<CreateUserCommand, Response<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPasswordHasherService _passwordHasherService;

        public CreateUserdHandler(IMapper mapper, IUserService userService, IPasswordHasherService passwordHasherService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _passwordHasherService = passwordHasherService ?? throw new ArgumentNullException(nameof(passwordHasherService));
        }

        public async Task<Response<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name=request.Name,
                Email = request.Email,
                Password = _passwordHasherService.Hash(request.Password),
                Role=request.Role,
                RefreshToken = null,
                IsActive = true
            };

            await _userService.CreateAsync(user);

            Response<UserDto> response = new Response<UserDto>() 
            { 
                Message="Success",
                IsValid=true,
                Results=null
            };

            return response;
        }
    }
}
