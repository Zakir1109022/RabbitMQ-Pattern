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
   public class LoginHandler : IRequestHandler<LoginCommand, TokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccessTokenService _tokenService;
        private readonly IUserService _userService;

        public LoginHandler(IMapper mapper, IAccessTokenService tokenService, IUserService userService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            TokenResponse response = new TokenResponse();

            //Validate user
            var users = await _userService.FilterByAsync(x => x.Email == request.Email && x.Password==request.Password);

            var userDtos = _mapper.Map<List<UserDto>>(users);

            if (userDtos.Count == 1)
            {
                var accessToken = _tokenService.GenerateJSONWebToken(userDtos[0]);
                var refreshToken = _tokenService.GenerateRefreshToken();

                //Update user
                var user = _mapper.Map<User>(userDtos[0]);
                user.RefreshToken = refreshToken;
                await _userService.UpdateAsync(user);

                //Set response
                response.AccessToken = accessToken;
                response.RefreshToken = refreshToken;
            }

            return response;
        }

    }
}
