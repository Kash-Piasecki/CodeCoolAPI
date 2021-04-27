using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.DAL.Models;

namespace CodeCoolAPI.DAL.Repositories
{
    public interface IMaterialRepository : IRepository<Material>
    {
        Task<IEnumerable<Material>> FindAllByQuery(string searchByTypeName);
    }
}