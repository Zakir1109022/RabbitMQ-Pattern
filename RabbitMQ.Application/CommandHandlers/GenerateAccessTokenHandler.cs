using AutoMapper;
using MediatR;
using RabbitMQ.Application.Commands;
using RabbitMQ.Application.Responses;
using RabbitMQ.Application.Services;
using RabbitMQ.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ.Application.CommandHandlers
{
  public  class GenerateAccessTokenHandler : IRequestHandler<GenerateAccessTokenCommand, TokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccessTokenService _tokenService;
        private readonly IUserService _userService;

        public GenerateAccessTokenHandler(IMapper mapper, IAccessTokenService tokenService, IUserService userService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<TokenResponse> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            TokenResponse response = new TokenResponse();

            string accessToken = request.AccessToken;
            string refreshToken = request.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return null;
            }

            string userId = principal.Identity.Name;

            var user = await _userService.GetByIdAsync(userId);

            if (user == null || user.RefreshToken != refreshToken)
            {
                //
            }


            var userDto = _mapper.Map<UserDto>(user);

            var newAccessToken = _tokenService.GenerateJSONWebToken(userDto);
            var newRefreshToken= _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userService.UpdateAsync(user);


            response.AccessToken = newAccessToken;
            response.RefreshToken = newRefreshToken;

            return response;
        }

    }
}
