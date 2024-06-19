using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TransportCompany.Interface;
using TransportCompany.Models;

namespace TransportCompany.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SymmetricSecurityKey _key;
        public UserService(IConfiguration config, UserManager<User> userManager) {
        _config= config;
        _userManager= userManager;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]!));
        }
        public async Task<string> CreateTokenJWT(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var rol = roles.FirstOrDefault();
            Console.WriteLine(roles);
            List<Claim> claims = new List<Claim>{ 
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Role, rol! )
                };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials
            };
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
        }
    }
}
