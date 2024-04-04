using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.WebApp.Network.Middleware;

namespace SocialNetwork.WebApp.Network.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ValidationUser _validate;

        public CommentController(ICommentService commentService, IUserService userService, IPostService postService, ValidationUser validate)
        {
            _commentService = commentService;
            _userService = userService;
            _postService = postService;
            _validate = validate;
        }
        public async Task<ActionResult> Index()
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _commentService.GetAll());
        }


        public async Task<IActionResult> Create(int id, int userId)
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            ViewBag.Comments = await _commentService.GetAllInclude(id);
            ViewBag.User = await _userService.GetById(userId);
            ViewBag.Post = await _postService.GetById(id);
            return View(new CommentSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _commentService.Create(vm);
            return RedirectToRoute(new {controller= "FriendShip", action= "Friends" });
        }

        public async Task<IActionResult> Update(int id)
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            var comment = await _commentService.GetById(id);
            return View("Create", comment);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CommentSaveViewModel vm)
        {
            await _commentService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "FriendShip", action = "Friends" });

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            var comment = await _commentService.GetById(id);
            return View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _commentService.Delete(id);
            return RedirectToRoute(new {controller= "FriendShip", action="Friends"});
        }


    }
}
