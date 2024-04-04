using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Helpers
{
    public static class ConvertEmbedVideo
    {
        public static string ConverterToEmbed(string videoUrl)
        {
            if (videoUrl == null)
            {
                return null;
            }
            if (videoUrl.Contains("www.youtube.com"))
            {

                videoUrl = videoUrl.Replace("watch?v=", "embed/");
            }
            else
            {
                videoUrl = videoUrl.Replace("youtu.be", "www.youtube.com/embed");
            }

            return videoUrl;
        }
    }
}
