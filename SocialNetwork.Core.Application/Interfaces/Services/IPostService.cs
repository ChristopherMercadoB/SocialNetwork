using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;


namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<PostViewModel, PostSaveViewModel, Post>
    {
        Task<List<PostViewModel>> GetPostByDate();
        Task<List<PostViewModel>> GetPostOfFriends();
        Task<PostSaveViewModel> CreateWithImage(PostSaveViewModel vm);

    }
}
