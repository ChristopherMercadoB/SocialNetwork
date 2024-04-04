using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Repositories;
using SocialNetwork.Infrastructure.Persistence.Context;
using SocialNetwork.Infrastructure.Persistence.Repositories;


namespace SocialNetwork.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(op=>op.UseInMemoryDatabase("UseInMemoryDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(op=>
                  op.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    m=>m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IReplyRepository, ReplyRepository>();
            services.AddTransient<IFriendShipRepository, FriendShipRepository>();
            #endregion
        }
    }
}
