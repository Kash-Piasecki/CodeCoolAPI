using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeCoolAPI.DAL.Repositories
{
    class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(CodecoolContext db) : base(db)
        {
        }
        
        public override async Task<Material> Find(int id)
        {
            return await _db.Materials.Include(x => x.Author).Include(x => x.MaterialType)
                .FirstOrDefaultAsync(z => z.Id == id);
        }
        
        public override async Task<IEnumerable<Material>> FindAll()
        {
            return await _db.Materials.Include(x => x.Author).Include(x => x.MaterialType)
                .ToListAsync();
        }
    }
}