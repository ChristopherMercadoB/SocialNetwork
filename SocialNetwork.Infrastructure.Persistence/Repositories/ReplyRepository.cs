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
    public class ReplyRepository : GenericRepository<Reply>, IReplyRepository
    {
        private readonly ApplicationContext _context;
        public ReplyRepository(ApplicationContext context):base(context)
        {
            _context = context;
        }

        
    }
}
