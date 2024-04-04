using SocialNetwork.Core.Application.Repositories;
using SocialNetwork.Core.Domain.Entities;


namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetByDate(List<string> properties);
    }
}
