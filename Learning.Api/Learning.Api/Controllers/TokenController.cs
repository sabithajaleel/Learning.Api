using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Learning.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Learning.Api.Repositories;

namespace Learning.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public TokenController(IConfiguration config, IUserRepository userRepository)
        {
            this.configuration = config;
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] LoginModel loginModel)
        {
            IActionResult response = Unauthorized();

            var user = await Authenticate(loginModel);

            if(user != null)
            {
                var token = BuildToken(user);
                response = Ok(new { FirstName = user.FirstName, LastName = user.LastName, Token = token });
            }

            return response;
        }

        private string BuildToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(this.configuration["Jwt:Issuer"],
              this.configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserModel> Authenticate(LoginModel login)
        {
            UserModel user = null;

            if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
            {
                return user;
            }

            var response = await this.userRepository.GetUserByEmail(login.Username);
            if (response == null)
            {
                return user;
            }

            if (response.Password != login.Password)
            {
                return user;
            }

            user = new UserModel
            {
                FirstName = response.FirstName,
                LastName = response.LastName,
                Email = response.Email,
                UserName = response.UserName
            };

            return user;
        }
    }
}
