using System.Linq;
using System.Threading.Tasks;
using CodeCoolAPI.CustomExceptions;
using CodeCoolAPI.DAL.Models;
using CodeCoolAPI.DAL.UnitOfWork;
using CodeCoolAPI.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace CodeCoolAPI.Services
{
    public interface IIdentityService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
        Task RegisterAdmin(RegisterUserDto registerUserDto);
    }

    class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public IdentityService(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
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
            var user = await _unitOfWork.Users.FindByCondition(x => x.Email == registerUserDto.Email);
            var isInDatabse = user.FirstOrDefault();
            if (isInDatabse is not null)
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
    }
}