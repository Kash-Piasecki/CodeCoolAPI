using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CodeCoolAPI.CustomExceptions;
using CodeCoolAPI.DAL.Models;
using CodeCoolAPI.DAL.UnitOfWork;
using CodeCoolAPI.Dtos.User;
using CodeCoolAPI.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CodeCoolAPI.Services
{
    public interface IIdentityService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
        Task RegisterAdmin(RegisterUserDto registerUserDto);
        Task<string> Login(LoginUserDto loginUserDto);
    }

    class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public IdentityService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            var user = await _unitOfWork.Users.FindByCondition(x => x.Email == registerUserDto.Email);
            var isInDatabse = user.FirstOrDefault();
            if (isInDatabse is not null)
                throw new BadRequestException("User already exists in the database");
            var userRoleId = 2;
            var newUser = new User()
            {
                Email = registerUserDto.Email,
                UserRoleId = userRoleId,
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            await _unitOfWork.Users.Create(newUser);
            await _unitOfWork.Save();
        }

        public async Task RegisterAdmin(RegisterUserDto registerUserDto)
        {
            var user = await _unitOfWork.Users.FindUserByEmail(registerUserDto.Email);
            if (user is not null)
                throw new BadRequestException("User already exists in the database");
            var adminRoleId = 1;
            var newUser = new User()
            {
                Email = registerUserDto.Email,
                UserRoleId = adminRoleId,
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            await _unitOfWork.Users.Create(newUser);
            await _unitOfWork.Save();
        }

        public async Task<string> Login(LoginUserDto loginUserDto)
        {
            var user = await _unitOfWork.Users.FindUserByEmail(loginUserDto.Email);
            if (user is null)
                throw new BadRequestException("Wrong credentials");
            var passwordVerificationResult =
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new BadRequestException("Wrong credentials");
            return await GenerateJwt(user);
        }

        private async Task<string> GenerateJwt(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
                claims, expires: expires,
                signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}