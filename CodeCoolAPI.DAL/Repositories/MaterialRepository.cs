using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Material>> FindAllByQuery(string searchByTypeName)
        {
            return await _db.Materials.Include(x => x.Author).Include(x => x.MaterialType).Where(x => x.MaterialType.Name.Contains(searchByTypeName))
                .ToListAsync();
        }

        public override async Task<IEnumerable<Material>> FindAll()
        {
            return await _db.Materials.Include(x => x.Author).Include(x => x.MaterialType)
                .ToListAsync();
        }
    }
}