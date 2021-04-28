using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeCoolAPI.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeCoolAPI.DAL.Repositories
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CodecoolContext _db;

        protected Repository(CodecoolContext db)
        {
            _db = db;
        }

        public virtual async Task<IEnumerable<T>> FindAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> predicate)
        {
            return await _db.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<int> Count()
        {
            return await _db.Set<T>().CountAsync();
        }

        public virtual async Task<T> Find(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task Create(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            await Task.Run(() => Console.WriteLine("Update"));
        }

        public async Task Delete(T entity)
        {
            await Task.Run(() => _db.Remove(entity));
        }
    }
}