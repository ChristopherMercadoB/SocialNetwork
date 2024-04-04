using Microsoft.AspNetCore.Http;


namespace SocialNetwork.Core.Application.Helpers
{
    public static class UploadFile
    {
        public static string UploadImage(IFormFile file, string directory, int id, bool editMode = false, string imageUrl = "")
        {
            if (editMode && file == null)
            {
                return imageUrl;
            }

            string basePath = $"/Images/{directory}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileName = guid + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

                if (editMode)
                {
                    string[] oldPath = imageUrl.Split("/");
                    string oldImage = oldPath[^1];
                    string completeOldPath = Path.Combine(basePath, oldImage);

                    if (File.Exists(completeOldPath))
                    {
                        File.Delete(completeOldPath);
                    }

                }

            return $"{basePath}/{fileName}";
        }

        public static void RemoveImage(string directory, int id)
        {
            string basePath = $"/Images/{directory}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}
