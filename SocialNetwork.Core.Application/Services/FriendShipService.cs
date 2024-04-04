using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Enums;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.FriendShip;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class FriendShipService : GenericService<FriendShipViewModel, FriendShipSaveViewModel, FriendShip>, IFriendShipService
    {

        private readonly IFriendShipRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly UserViewModel _vm;
        private readonly IMapper _mapper;
        

        public FriendShipService(IMapper mapper, IFriendShipRepository repository, IUserRepository userRepository, IHttpContextAccessor accessor) : base(mapper, repository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _vm = accessor.HttpContext.Session.GetSession<UserViewModel>(CurrentUser.User.ToString());
        }

        public async Task DeleteByFriends(int id)
        {
            var friend = await _repository.GetFriendById(id, _vm.Id);
            await _repository.DeleteAsync(friend);
        }

        public async Task<List<UserViewModel>> FilterFriendShip(FilterFriendShipViewModel vm)
        {
            var friendships = await _repository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();

            var userFriends = friendships
                .Where(f => f.CurrentUserId == _vm.Id || f.UserFriendId == _vm.Id)
                .Select(f => f.CurrentUserId == _vm.Id ? f.UserFriendId : f.CurrentUserId)
                .ToList();

            var list = users
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

            if (vm.Username != null)
            {
                list = list.Where(u=>u.Username.StartsWith(vm.Username) || u.Username.Contains(vm.Username)).ToList();
            }

            return list;
        }

        public async Task Follow(int id)
        {
            FriendShipSaveViewModel vm = new();
            vm.UserFriendId = id;
            vm.CurrentUserId = _vm.Id;
            var entity = _mapper.Map<FriendShip>(vm);
            await _repository.AddAsync(entity);
        }

        public async Task<FriendShipSaveViewModel> GetByFriendId(int id)
        {
            var friend = await _repository.GetFriendById(id, _vm.Id);
            return _mapper.Map<FriendShipSaveViewModel>(friend);
        }
    }
}
