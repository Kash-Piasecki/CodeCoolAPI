using System.Threading.Tasks;
using CodeCoolAPI.DAL.Repositories;

namespace CodeCoolAPI.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IAuthorRepository Authors { get; set; }
        public IMaterialRepository Materials { get; set; }
        public IMaterialTypeRepository MaterialTypes { get; set; }
        public IReviewRepository Reviews { get; set; }
        public IUserRepository Users { get; set; }

        Task Save();
    }
}