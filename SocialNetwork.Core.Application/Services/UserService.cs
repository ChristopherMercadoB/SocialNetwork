using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Enums;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Core.Application.DTO_S;
using SocialNetwork.Core.Application.DTO_S.Email;

namespace SocialNetwork.Core.Application.Services
{
    public class UserService : GenericService<UserViewModel, UserSaveViewModel, User>, IUserService
    {

        private readonly IUserRepository _repository;
        private readonly IFriendShipRepository _friendRepostiory;
        private readonly IMapper _mapper;
        private readonly UserViewModel _vm;
        private readonly IEmailService _mailService;
        public UserService(IMapper mapper, IUserRepository repository, IFriendShipRepository friendRepository, IHttpContextAccessor accessor, IEmailService mailService) : base(mapper, repository)
        {
            _repository = repository;
            _mapper = mapper;
            _friendRepostiory = friendRepository;
            _vm = accessor.HttpContext.Session.GetSession<UserViewModel>(CurrentUser.User.ToString());
            _mailService = mailService;

        }

        public async Task<UserSaveViewModel> CreateWithImage(UserSaveViewModel vm)
        {
            User user = _mapper.Map<User>(vm);
            user = await _repository.AddAsync(user);
            UserSaveViewModel uvm = _mapper.Map<UserSaveViewModel>(user);
            uvm.File = vm.File;
            return uvm;
        }

        public async Task ForgotPassword(ForgotPasswordViewModel vm)
        {
            var entity = await _repository.ForgotPassword(vm);

            if (entity != null)
            {
                Random rand = new Random();
                string pass = rand.Next(100, 1000).ToString();
                entity.Password = PasswordEncryption.ComputeSha256Hash(pass);
                await _repository.UpdateAsync(entity, entity.Id);
                await _mailService.SendEmailAsync(new MailRequest
                {
                    To = entity.Email,
                    Subject = "Nueva contrasena",
                    Body = $"Su nueva contrasena es: {pass}"
                });
            }
            
        }

        public async Task<List<UserViewModel>> GetFriendsOfCurrentUser()
        {
            var friendships = await _friendRepostiory.GetByUserId(_vm.Id);

            var friendIds = friendships
                .SelectMany(f => new[] { f.CurrentUserId, f.UserFriendId })
                .Where(id => id != _vm.Id)
                .Distinct()
                .ToList();


            var friends = await _repository.GetUsersByIdsAsync(friendIds);

            var friendViewModels = friends.Select(friend => new UserViewModel
            {
                Id = friend.Id,
                Name = friend.Name,
                LastName = friend.LastName,
                ImageUrl = friend.ImageUrl,
                Email = friend.Email,
                Username = friend.Username,
            }).ToList();

            return friendViewModels;
        }


        public async Task<List<UserViewModel>> GetSuggestionForFriends()
        {
            var friendships = await _friendRepostiory.GetAllAsync();
            var users = await _repository.GetAllAsync();

            var userFriends = friendships
                .Where(f => f.CurrentUserId == _vm.Id || f.UserFriendId == _vm.Id)
                .Select(f => f.CurrentUserId == _vm.Id ? f.UserFriendId : f.CurrentUserId)
                .ToList();

            var suggestions = users
                .Where(u => u.Id != _vm.Id && !userFriends.Contains(u.Id))
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastName = x.LastName,
                    ImageUrl = x.ImageUrl,
                    Email = x.Email,
                    Username = x.Username,
                }).ToList();

            return suggestions;

        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            var user = await _repository.LoginAsync(vm);
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserViewModel>(user);
        }
    }
}

