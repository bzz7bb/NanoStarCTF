using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NanoStarCTF.DataAccess.Repositories;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.Extensions;
using NanoStarCTF.Service.IService;
using NanoStarCTF.Service.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NanoStarCTF.Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IConfiguration configuration;

        public AuthService(IGenericRepository<User> userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        public async ValueTask<string> GenerateToken(string email, string password)
        {
            var user = await userRepository.GetAsync(p => p.Email.ToLower() == email.ToLower())
            ?? throw new HttpStatusCodeException(400, "User not found");

            if (!SecurePasswordHasher.Verify(password, user.Password))
                throw new HttpStatusCodeException(200, "Email or password is wrong");


            if (user is null)
                throw new HttpStatusCodeException(400, "Login or Password is incorrect");

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddHours(int.Parse(configuration["JWT:Expire"])),
                claims: new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.ToString())
                },
                signingCredentials: new SigningCredentials(
                    key: authSigningKey,
                    algorithm: SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
