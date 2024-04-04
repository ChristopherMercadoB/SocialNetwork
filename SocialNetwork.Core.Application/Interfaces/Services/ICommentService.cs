using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;


namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface ICommentService : IGenericService<CommentViewModel, CommentSaveViewModel, Comment>
    {
        Task<List<CommentViewModel>> GetAllInclude(int id);
    }
}
