using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace SocialNetwork.Core.Application.Helpers
{
    public static class PasswordEncryption
    {
        public static string ComputeSha256Hash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new();
                foreach (var item in bytes)
                {
                    builder.Append(item.ToString("x2"));
                }

                return builder.ToString();
            }            
        }
    }
}
