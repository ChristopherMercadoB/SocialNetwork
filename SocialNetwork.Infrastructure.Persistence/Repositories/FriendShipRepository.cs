using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class FriendShipRepository : GenericRepository<FriendShip>, IFriendShipRepository
    {
        private readonly ApplicationContext _context;
        public FriendShipRepository(ApplicationContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<FriendShip>> GetByUserId(int id)
        {
            return await _context.Set<FriendShip>()
            .Where(f => f.CurrentUserId == id || f.UserFriendId == id)
            .ToListAsync();
        }

        public async Task<FriendShip> GetFriendById(int id, int currentUserId)
        {
            return await _context.Set<FriendShip>().FirstOrDefaultAsync(u => (u.CurrentUserId == currentUserId && u.UserFriendId == id) || ((u.CurrentUserId == id && u.UserFriendId == currentUserId)));
        }

    }
}
