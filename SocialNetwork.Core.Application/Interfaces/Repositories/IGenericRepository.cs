
namespace SocialNetwork.Core.Application.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllInclude(List<string> properties);
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T entity, int id);
        Task DeleteAsync(T entity);
    }
}