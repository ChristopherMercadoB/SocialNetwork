using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Context;


namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class UserRepository:GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context):base(context)
        {
            _context = context;
        }

        public override async Task<User> AddAsync(User user)
        {
            user.Password = PasswordEncryption.ComputeSha256Hash(user.Password);
            await base.AddAsync(user);
            return user;
        }

        public async Task<User> ForgotPassword(ForgotPasswordViewModel vm)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u=>u.Username == vm.UserName);
        }

        

        public async Task<List<User>> GetUsersByIdsAsync(List<int> userIds)
        {
            return await _context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();
        }

        public async Task<User> LoginAsync(LoginViewModel vm)
        {
            vm.Password = PasswordEncryption.ComputeSha256Hash(vm.Password);
            User user = await _context.Set<User>()
                .FirstOrDefaultAsync(u=>u.Username == vm.Username && u.Password == vm.Password && u.State == true);
            return user;
        }

  
    }
}
