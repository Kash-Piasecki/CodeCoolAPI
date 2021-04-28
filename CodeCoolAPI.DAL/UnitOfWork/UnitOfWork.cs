using System.Threading.Tasks;
using CodeCoolAPI.DAL.Context;
using CodeCoolAPI.DAL.Repositories;

namespace CodeCoolAPI.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CodecoolContext _db;
        public IAuthorRepository Authors { get; set; }
        public IMaterialRepository Materials { get; set; }
        public IMaterialTypeRepository MaterialTypes { get; set; }
        public IReviewRepository Reviews { get; set; }
        public IUserRepository Users { get; set; }

        public UnitOfWork(CodecoolContext db)
        {
            _db = db;
            Authors = new AuthorRepository(_db);
            Materials = new MaterialRepository(_db);
            MaterialTypes = new MaterialTypeRepository(_db);
            Reviews = new ReviewRepository(_db);
            Users = new UserRepository(_db);
        }
        
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}