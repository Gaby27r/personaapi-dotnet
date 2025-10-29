using Microsoft.EntityFrameworkCore;
using personaapi_dotnet.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace personaapi_dotnet.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PersonaDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(PersonaDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(object id) =>
            await _dbSet.FindAsync(id);

        public async Task<T?> GetByIdsAsync(params object[] ids) =>
            await _dbSet.FindAsync(ids);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }

}
