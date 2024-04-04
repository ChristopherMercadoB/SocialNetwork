using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Enums;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Context;

namespace SocialNetwork.WebApp.Network.Middleware
{
    public class ValidationUser
    {
        private readonly UserViewModel _vm;
        private readonly ApplicationContext _context;
        public ValidationUser(IHttpContextAccessor accessor, ApplicationContext context)
        {
            _vm = accessor.HttpContext.Session.GetSession<UserViewModel>(CurrentUser.User.ToString());
            _context = context;
        }

        public async Task<bool> ValidateIfUserExist(ForgotPasswordViewModel vm)
        {
            var user = await _context.Set<User>()
                .FirstOrDefaultAsync(u=>u.Username == vm.UserName);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public bool ValidateIfUserSession()
        {
            if (_vm == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateIsRegistered(UserSaveViewModel vm)
        {
            var user = await _context.Set<User>()
               .FirstOrDefaultAsync(u => u.Username == vm.Username);

            if (user == null)
            {
                return false;
            }

            return true;
        }

    }
}
