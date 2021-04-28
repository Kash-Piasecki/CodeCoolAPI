using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CodeCoolAPI.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CodeCoolAPI.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> Register(string email, string password);
        Task<AuthenticationResult> Login(string email, string password);
    }

    class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> Register(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser is not null)
                return new AuthenticationResult()
                {
                    Errors = new[] {"User with this email address already exists"}
                };
            var newUser = new IdentityUser()
            {
                Email = email,
                UserName = email,
            };
            
            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult()
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return GenerateAuthenticationResult(newUser);
        }

        public async Task<AuthenticationResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (user is null || !userHasValidPassword)
            {
                return new AuthenticationResult()
                {
                    Errors = new[] {"Wrong credentials"}
                };
            }

            return GenerateAuthenticationResult(user);
        }
        
        private AuthenticationResult GenerateAuthenticationResult(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim("id", newUser.Id),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult()
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

    }
}