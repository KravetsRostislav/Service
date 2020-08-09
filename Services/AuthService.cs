using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services;
using Shop.API.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Shop.API.Domain.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Shop.API.Extensions;

namespace Shop.API.Services
{
    public class AuthService : IAuthService
    {

        private readonly AppSettings appSettings;
        private readonly IUserRepository userRepository;
        public AuthService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
        }
        public async Task<User> Authenticate(string login, string password)
        {
            var user = (await userRepository.ListAsync())
                                .SingleOrDefault(usr => usr.Login == login &&
                                                        usr.Password == password);  
            if (user == null)
                return null;

            user.GenerateToken(appSettings.Secret, appSettings.ExpiresMinutes);

            return user;
        }
    }
}