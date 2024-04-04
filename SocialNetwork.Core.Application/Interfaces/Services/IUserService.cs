using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;


namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserService:IGenericService<UserViewModel, UserSaveViewModel, User>
    {
        Task<UserViewModel> Login(LoginViewModel vm);
        Task<UserSaveViewModel> CreateWithImage(UserSaveViewModel vm);
        Task<List<UserViewModel>> GetSuggestionForFriends();
        Task<List<UserViewModel>> GetFriendsOfCurrentUser();
        Task ForgotPassword(ForgotPasswordViewModel vm);
    }
}
