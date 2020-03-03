﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Tariff.Identity.Helpers;
using Tariff.Identity.Models;

namespace Tariff.Identity.Services
{
    public interface IUserService
    {
        Models.SecurityToken Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { UserId = 1234567, FullName = "John Smith", Username = "johnsmith", Password = "john1234" },
            new User { UserId = 7654321, FullName = "Anna Brooke", Username = "anna", Password = "anna7654" },
            new User { UserId = 1859624, FullName = "Jack White", Username = "jackwhite", Password = "jack1859" }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Models.SecurityToken Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userid", user.UserId.ToString()),
                    new Claim("name", user.FullName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtSecurityToken = tokenHandler.WriteToken(token);

            return new Models.SecurityToken() { Auth_token = jwtSecurityToken };
        }
    }
}
