using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.Models;

namespace CodeCoolAPI.DAL.Repositories
{
    class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        public MaterialRepository(CodecoolContext db) : base(db)
        {
        }
    }
}