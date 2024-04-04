using SocialNetwork.Core.Application.Repositories;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;


namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IFriendShipRepository : IGenericRepository<FriendShip>
    {
        Task<List<FriendShip>> GetByUserId(int id);
        Task<FriendShip> GetFriendById(int id, int currentUserId);
    }
}
