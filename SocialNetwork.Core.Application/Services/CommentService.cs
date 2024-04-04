using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Enums;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class CommentService : GenericService<CommentViewModel, CommentSaveViewModel, Comment>, ICommentService
    {

        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserViewModel _vm;

        public CommentService(IMapper mapper, ICommentRepository repository, IHttpContextAccessor accessor):base(mapper, repository)
        {
            _repository = repository;
            _mapper = mapper;
            _vm = accessor.HttpContext.Session.GetSession<UserViewModel>(CurrentUser.User.ToString());
        }

        public async Task<List<CommentViewModel>> GetAllInclude(int id)
        {
            var comments = await _repository.GetAllInclude(new List<string> { "User"});
            return comments.Where(c=>c.PostId == id).Select(x=>new CommentViewModel
            {
                Id = x.Id,
                Description = x.Description,
                PostId = x.PostId,
                UserId = x.UserId,
                UserImage = x.User.ImageUrl,
                Username = x.User.Username
            }).ToList();
        }
    }
}
