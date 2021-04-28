using System.Security.Cryptography;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos.User;

namespace CodeCoolAPI.Services
{
    public interface IIdentityService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
        Task RegisterAdmin(RegisterUserDto registerUserDto);
        Task<string> Login(LoginUserDto loginUserDto);
    }
}