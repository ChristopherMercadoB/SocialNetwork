using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Enums;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class PostService : GenericService<PostViewModel, PostSaveViewModel, Post>, IPostService
    {

        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserViewModel _vm;
        private readonly IFriendShipRepository _friendRepository;
        public PostService(IMapper mapper, IFriendShipRepository friendRepository, IPostRepository repository, IHttpContextAccessor accessor) : base(mapper, repository)
        {
            _repository = repository;
            _friendRepository = friendRepository;
            _mapper = mapper;
            _vm = accessor.HttpContext.Session.GetSession<UserViewModel>(CurrentUser.User.ToString());

        }

        public async Task<PostSaveViewModel> CreateWithImage(PostSaveViewModel vm)
        {
            var post = _mapper.Map<Post>(vm);
            post.UserId = _vm.Id;
            post = await _repository.AddAsync(post);
            PostSaveViewModel pvm = _mapper.Map<PostSaveViewModel>(post);
            pvm.File = vm.File;
            return pvm;
        }

        public async Task<List<PostViewModel>> GetPostByDate()
        {
            List<Post> post = await _repository.GetByDate(new List<string> { "User"});
            return post.Where(p=>p.UserId == _vm.Id)
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    VideoUrl = p.VideoUrl,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    CreatedDate = p.CreatedDate,
                    Username = p.User.Username,
                    ImageUser = p.User.ImageUrl
                }).ToList();
        }

        public async Task<List<PostViewModel>> GetPostOfFriends()
        {
            var friendships = await _friendRepository.GetAllAsync();

            var posts = await _repository.GetByDate(new List<string> { "User" });

            var friendIds = friendships
                .Where(f => f.CurrentUserId == _vm.Id || f.UserFriendId == _vm.Id)
                .SelectMany(f => new[] { f.CurrentUserId, f.UserFriendId })
                .Distinct()
                .ToList();

            var friendPosts = posts
                .Where(p => friendIds.Contains(p.UserId))
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    VideoUrl = p.VideoUrl,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    CreatedDate = p.CreatedDate,
                    Username = p.User.Username,
                    ImageUser = p.User.ImageUrl
                }).ToList();

            return friendPosts;
        }
    }
    
}
