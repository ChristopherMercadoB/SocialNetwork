using SocialNetwork.Core.Application.ViewModels.FriendShip;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;


namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IFriendShipService : IGenericService<FriendShipViewModel, FriendShipSaveViewModel, FriendShip>
    {
        Task<List<UserViewModel>> FilterFriendShip(FilterFriendShipViewModel vm);
        Task Follow(int id);
        Task<FriendShipSaveViewModel> GetByFriendId(int id);
        Task DeleteByFriends(int id);


    }
}
