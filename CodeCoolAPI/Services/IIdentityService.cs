using System.Linq;
using System.Threading.Tasks;
using CodeCoolAPI.CustomExceptions;
using CodeCoolAPI.DAL.Models;
using CodeCoolAPI.DAL.UnitOfWork;
using CodeCoolAPI.Dtos.User;

namespace CodeCoolAPI.Services
{
    public interface IIdentityService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
    }

    class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.Users.Create(newUser);
            await _unitOfWork.Save();
        }
    }
}