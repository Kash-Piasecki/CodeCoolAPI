using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCoolAPI.DAL.Repositories
{
    class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(CodecoolContext db) : base(db)
        {
        }
        
        public override async Task<Author> Find(int id)
        {
            return await _db.Authors.Include(x => x.Materials)
                .FirstOrDefaultAsync(z => z.Id == id);
        }
        
        public override async Task<IEnumerable<Author>> FindAll()
        {
            return await _db.Authors.Include(x => x.Materials)
                .ToListAsync();
        }
    }
}