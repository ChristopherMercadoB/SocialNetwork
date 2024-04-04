using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Domain.Settings;
using SocialNetwork.Infrastructure.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureShared(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configure
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            #endregion

            #region Service
            services.AddTransient<IEmailService, EmailService>();
            #endregion

        }
    }
}
