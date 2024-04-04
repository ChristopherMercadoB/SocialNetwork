using SocialNetwork.Core.Application.DTO_S.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest request);
    }
}
