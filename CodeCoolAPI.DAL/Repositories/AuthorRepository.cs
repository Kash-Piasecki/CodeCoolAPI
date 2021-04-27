using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.Models;

namespace CodeCoolAPI.DAL.Repositories
{
    class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(CodecoolContext db) : base(db)
        {
        }
    }
}