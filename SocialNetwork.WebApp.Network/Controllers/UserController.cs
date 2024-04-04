using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Enums;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.WebApp.Network.Middleware;

namespace SocialNetwork.WebApp.Network.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ValidationUser _validate;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IPostService postService, ValidationUser validate, IMapper mapper, IEmailService emailService)
        {
            _userService = userService;
            _postService = postService;
            _validate = validate;
            _mapper = mapper;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmAccount(int id)
        {
            var user = await _userService.GetById(id);
            user.State = true;
            await _userService.Update(user, id);
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            UserViewModel user = await _userService.Login(vm);
            if (user != null)
            {
                HttpContext.Session.SetSession<UserViewModel>(CurrentUser.User.ToString(), user);
                return RedirectToRoute(new {controller="Home", action="Index"});

            }
            return View(vm);
        }

        public IActionResult Register()
        {
            return View(new UserSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", vm);
            }
            if (!await _validate.ValidateIsRegistered(vm))
            {
                var userVm = await _userService.CreateWithImage(vm);
                if (userVm.File != null && userVm.Id != null)
                {
                    userVm.ImageUrl = UploadFile.UploadImage(vm.File, "User", userVm.Id);
                    await _userService.Update(userVm, userVm.Id);
                }
                string confirmAccountUrl = Url.Action("ConfirmAccount", "User", new { id = userVm.Id }, protocol: HttpContext.Request.Scheme);
                await _emailService.SendEmailAsync(new Core.Application.DTO_S.Email.MailRequest
                {
                    To = vm.Email,
                    Subject = "Activate Account",
                    Body = $"Click aqui para activar tu cuenta: {confirmAccountUrl}"
                });
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Ya hay un usuario registrado con ese username");
            }

            return View(vm);


        }

        public IActionResult Logout()
        {
            var user = HttpContext.Session.GetSession<UserViewModel>(CurrentUser.User.ToString());
            if (user != null)
            {
                HttpContext.Session.Remove(CurrentUser.User.ToString());
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Profile()
        {
            ViewBag.Suggestions = await _userService.GetSuggestionForFriends();
            ViewBag.Posts = await _postService.GetPostByDate();
            return View(HttpContext.Session.GetSession<UserViewModel>(CurrentUser.User.ToString()));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _userService.Delete(id);
            HttpContext.Session.Remove(CurrentUser.User.ToString());
            UploadFile.RemoveImage("User", id);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel ());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (await _validate.ValidateIfUserExist(vm))
            {
                await _userService.ForgotPassword(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> Update(int id)
        {
            var user = await _userService.GetById(id);
            return View("Register", user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserSaveViewModel vm)
        {
            var user = await _userService.GetById(vm.Id);
            if (!ModelState.IsValid)
            {
                return View("Register", vm);
            }
 
            string image = user.ImageUrl;
            string password = user.Password;
            if (user != null)
            {
                if (vm.Password == null)
                {
                    user = _mapper.Map<UserSaveViewModel>(vm);
                    user.Password = password;
                    user.ImageUrl = UploadFile.UploadImage(vm.File, "User", user.Id, true, image);
                }
                else if (vm.Password != null)
                {
                    user = _mapper.Map<UserSaveViewModel>(vm);
                    user.Password = PasswordEncryption.ComputeSha256Hash(vm.Password);
                    user.ImageUrl = UploadFile.UploadImage(vm.File, "User", user.Id, true, image);
                }
                
                await _userService.Update(vm, vm.Id);
            }

            return RedirectToRoute(new { controller = "User", action = "Profile" });

        }
    }
}
