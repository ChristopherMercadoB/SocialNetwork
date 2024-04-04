using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Repositories;
using SocialNetwork.Infrastructure.Persistence.Context;


namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity, int id)
        {
            var entry = await this.GetByIdAsync(id);
            _context.Entry(entry).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllInclude(List<string> properties)
        {
            var query = _context.Set<T>().AsQueryable();
            if (properties != null)
            {
                foreach (var item in properties)
                {
                    query = query.Include(item);
                }
            }

            return await query.ToListAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

       
    }
}
