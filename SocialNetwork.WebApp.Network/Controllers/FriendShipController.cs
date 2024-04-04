using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Enums;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.FriendShip;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.WebApp.Network.Middleware;


namespace SocialNetwork.WebApp.Network.Controllers
{
    public class FriendShipController : Controller
    {
        private readonly IFriendShipService _friendShip;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ValidationUser _validate;
        public FriendShipController(IFriendShipService service, IMapper mapper, IPostService postService, IUserService userService, ValidationUser validate)
        {
            _friendShip = service;
            _mapper = mapper;
            _userService = userService;
            _postService = postService;
            _validate = validate;
        }

        public async Task<IActionResult> Index(FilterFriendShipViewModel vm)
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            var filters = await _friendShip.FilterFriendShip(vm);
            return View(filters);
        }

        
        public async Task<IActionResult> Follow(int id)
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _friendShip.Follow(id);
            return View("Friends");
        }

        public async Task<IActionResult> Friends()
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            ViewBag.Users = await _userService.GetFriendsOfCurrentUser();
            ViewBag.Posts = await _postService.GetPostOfFriends();
            return View(HttpContext.Session.GetSession<UserViewModel>(CurrentUser.User.ToString()));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            var post = await _friendShip.GetByFriendId(id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _friendShip.Delete(id);
            return RedirectToRoute(new { controller = "FriendShip", action = "Friends" });
        }
    }
}
