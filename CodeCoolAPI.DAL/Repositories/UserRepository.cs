using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.Models;

namespace CodeCoolAPI.DAL.Repositories
{
    class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CodecoolContext db) : base(db)
        {
        }
    }
}