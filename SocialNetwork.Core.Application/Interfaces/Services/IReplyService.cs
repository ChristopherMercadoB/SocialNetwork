using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.Reply;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;


namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IReplyService : IGenericService<ReplyViewModel, ReplySaveViewModel, Reply>
    {
       
    }
}
