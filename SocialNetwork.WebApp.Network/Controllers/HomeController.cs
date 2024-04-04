using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Enums;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.WebApp.Network.Models;
using System.Diagnostics;
using SocialNetwork.WebApp.Network.Middleware;

namespace SocialNetwork.WebApp.Network.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ValidationUser _validate;
        public HomeController(IUserService userService, IPostService postService, ValidationUser validate)
        {
            _userService = userService;
            _postService = postService;
            _validate = validate;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new {controller="User", action="Index"});
            }
            ViewBag.Suggestions = await _userService.GetSuggestionForFriends();
            ViewBag.Posts = await _postService.GetPostByDate();
            return View(HttpContext.Session.GetSession<UserViewModel>(CurrentUser.User.ToString()));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
