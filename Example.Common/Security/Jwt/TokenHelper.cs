#region

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Example.Common.Helpers;
using Example.Common.Security.Jwt.Abstract;
using Example.Common.Security.Jwt.Models;
using Example.Entities.Entities;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace Example.Common.Security.Jwt
{
    public class TokenHelper : ITokenHelper
    {
        private readonly DateTime _expirationDate;
        private readonly TokenOptions _tokenOptions;

        public TokenHelper(TokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
            _expirationDate = DateTime.UtcNow.AddMinutes(_tokenOptions.Expiration);
        }

        public AccessToken CreateToken(User user)
        {
            var securityKey = _tokenOptions.Key.CreateSecurityKey();
            var signingCredentials = securityKey.CreateSigningCredentials();
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _expirationDate
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _expirationDate,
                notBefore: DateTime.UtcNow,
                claims: SetClaims(user),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.EMail);
            claims.AddName($"{user.FirstName} {user.LastName}");
            // roles are no longer added to token, they will be checked from DB on each request
            return claims;
        }
    }
}