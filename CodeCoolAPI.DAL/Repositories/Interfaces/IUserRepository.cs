using System.Threading.Tasks;
using CodeCoolAPI.DAL.Models;

namespace CodeCoolAPI.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindUserByEmail(string email);
    }
}