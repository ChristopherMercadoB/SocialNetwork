using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.WebApp.Network.Middleware;

namespace SocialNetwork.WebApp.Network.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ValidationUser _validate;
        public PostController(IPostService postService, IMapper mapper, ValidationUser validate)
        {
            _postService = postService;
            _mapper = mapper;
            _validate = validate;
        }
        public async Task<ActionResult> Index()
        {
            if (!_validate.ValidateIfUserSession())
            {
               return RedirectToRoute(new {controller= "User", action="Index"});
            }
            return View(await _postService.GetAll());
        }


        public IActionResult Create()
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(new PostSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var postVm = await _postService.CreateWithImage(vm);
            if (postVm.File != null && postVm.Id != null)
            {
                postVm.ImageUrl = UploadFile.UploadImage(vm.File, "Post", postVm.Id);
                await _postService.Update(postVm, postVm.Id);
            }
            else if (postVm.VideoUrl != null)
            {
                postVm.VideoUrl = ConvertEmbedVideo.ConverterToEmbed(vm.VideoUrl);
                await _postService.Update(postVm, postVm.Id);
            }
            return RedirectToRoute(new {controller= "Home", action="Index"});
        }

        public async Task<IActionResult> Update(int id)
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            var post = await _postService.GetById(id);
            return View("Create", post);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PostSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", vm);
            }
            var post = await _postService.GetById(vm.Id);
            string image = post.ImageUrl;
            if (post.File != null)
            {
                post = _mapper.Map<PostSaveViewModel>(vm);
                post.ImageUrl = UploadFile.UploadImage(vm.File, "Post", post.Id, true, image);
                await _postService.Update(vm, vm.Id);
            }
            else if (post != null && post.VideoUrl != null)
            {
                post.VideoUrl = ConvertEmbedVideo.ConverterToEmbed(vm.VideoUrl);
                await _postService.Update(post, post.Id);
            }
            
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validate.ValidateIfUserSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            var post = await _postService.GetById(id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.Delete(id);
            UploadFile.RemoveImage("Post", id);
            return RedirectToRoute(new {controller="Home", action="Index"});
        }


    }
}
