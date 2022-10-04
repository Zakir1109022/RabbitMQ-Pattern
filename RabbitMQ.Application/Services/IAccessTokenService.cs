using RabbitMQ.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace RabbitMQ.Application.Services
{
   public interface IAccessTokenService
    {
        string GenerateJSONWebToken(UserDto userInfo);
        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string? token);
    }
}
