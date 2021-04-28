using System.Threading.Tasks;
using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCoolAPI.DAL.Repositories
{
    class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CodecoolContext db) : base(db)
        {
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _db.Users.Include(x => x.UserRole).FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}