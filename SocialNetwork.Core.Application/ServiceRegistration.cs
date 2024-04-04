using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Services;
using System;
using System.Reflection;


namespace SocialNetwork.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddCoreApplication(this IServiceCollection services, IConfiguration configuration)
        {
            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IFriendShipService, FriendShipService>();
            services.AddTransient<IReplyService, ReplyService>();
            #endregion
        }
    }
}
