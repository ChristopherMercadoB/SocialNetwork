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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly ApplicationContext _context;
        public PostRepository(ApplicationContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetByDate(List<string> properties)
        {
            var query = _context.Set<Post>().AsQueryable();
            foreach (var item in properties)
            {
                query = query.Include(item);
            }
            return await query.OrderByDescending(p=>p.CreatedDate).ToListAsync();
        }
    }
}
