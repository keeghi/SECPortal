using SecPortal.Entities.Data;
using SecPortal.Webapp.CQRS.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecPortal.Webapp.Authorizations
{
    public class JwtAuthorization : IAuthorization
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public JwtAuthorization(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<IBaseResponse> Login(string username, string password)
        {
            var response = BaseResponse.Factory.Build();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !user.IsActive)
            {
                response.IsSuccess = false;
                response.Message = "User not found.";
            }
            else if (!user.EmailConfirmed)
            {
                response.IsSuccess = false;
                response.Message = "The given email address has not been confirmed. Please verify it by clicking the activation link that has been sent to your email.";
            }
            else if (await _userManager.CheckPasswordAsync(user, password))
            {
                return CreateToken(user);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Wrong password.";
            }

            return response;
        }


        private JwtResponse CreateToken(ApplicationUser user)
        {
            var task = Task.Run(async () => await _userManager.GetRolesAsync(user));
            var role = task.Result;

            List<Claim> claims = new List<Claim>
            {
                new Claim("roles", role.FirstOrDefault()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expiry = DateTime.Now.AddHours(8);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiry,
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            var response = new JwtResponse(user.Id.ToString(), role.FirstOrDefault(), tokenHandler.WriteToken(token), expiry, user.UserName, user.Name);
            return response;
        }
    }
}
