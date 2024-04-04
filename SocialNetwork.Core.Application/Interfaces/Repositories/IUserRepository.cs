using SocialNetwork.Core.Application.Repositories;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel vm);
        Task<User> ForgotPassword(ForgotPasswordViewModel vm);
        Task<List<User>> GetUsersByIdsAsync(List<int> userIds);
    }
}
