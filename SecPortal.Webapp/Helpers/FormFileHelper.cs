using SecPortal.Commons.Exceptions;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SecPortal.Webapp.Helpers
{
    public class FileNameHolder
    {
        public string ThumbName { get; set; }
        public string FileName { get; set; }

        public FileNameHolder()
        {

        }

        public FileNameHolder(string thumbName, string fileName)
        {
            ThumbName = thumbName;
            FileName = fileName;
        }
    }

    public static class FormFileHelper
    {
        private static string[] _acceptedExt = new string[] { ".jpg", ".png", ".jpeg" };

        public static void RemoveImage(string fileName)
        {
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/attachments", fileName);
            if (File.Exists(savePath))
            {
                try
                {
                    File.Delete(savePath);
                }
                catch (Exception)
                {
                    // intended to be blank
                }
            }
        }

        public static async Task<string> SaveImageAsync(IFormFile formFile)
        {
            var generatedFilename = $"{Guid.NewGuid()}-{DateTime.Now:ddMMyyyyHHmmss}{Path.GetExtension(formFile.FileName)}";
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/attachments", generatedFilename);

            if (!_acceptedExt.Contains(Path.GetExtension(formFile.FileName)))
            {
                throw new CustomException("File extension not accepted");
            }

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            }

            using (var stream = File.Create(savePath))
            {
                await formFile.CopyToAsync(stream);
            }

            return generatedFilename;
        }

        public static async Task<string> SaveThumbsAsync(IFormFile formFile)
        {
            var generatedFilename = $"{Guid.NewGuid()}-{DateTime.Now:ddMMyyyyHHmmss}-thumb{Path.GetExtension(formFile.FileName)}";
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/attachments", generatedFilename);

            if (!_acceptedExt.Contains(Path.GetExtension(formFile.FileName)))
            {
                throw new CustomException("File extension not accepted");
            }

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            }

            using (var stream = File.Create(savePath))
            {
                await formFile.CopyToAsync(stream);
            }

            CreateThumbnail(savePath, savePath); // Overwrite the image

            return generatedFilename;
        }

        private static void CreateThumbnail(string filePath, string savePath)
        {
            using (var image = Image.Load(filePath))
            {
                image.Mutate(x => x
                     .Resize(image.Width / 2, image.Height / 2));
                image.Save(savePath); // Automatic encoder selected based on extension.
            }
        }

        public static async Task<FileNameHolder> SaveImageAndGenerateThumb(IFormFile formFile)
        {
            var guid = Guid.NewGuid();
            var date = DateTime.Now;
            var generatedFilename = $"{guid}-{date:ddMMyyyyHHmmss}{Path.GetExtension(formFile.FileName)}";
            var generatedThumbs = $"{guid}-{date:ddMMyyyyHHmmss}-thumb{Path.GetExtension(formFile.FileName)}";
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/attachments", generatedFilename);
            var saveThumbPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/attachments", generatedThumbs);

            if (!_acceptedExt.Contains(Path.GetExtension(formFile.FileName)))
            {
                throw new CustomException("File extension not accepted");
            }

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            }

            using (var stream = File.Create(savePath))
            {
                await formFile.CopyToAsync(stream);
            }

            CreateThumbnail(savePath, saveThumbPath);
            return new FileNameHolder(generatedThumbs, generatedFilename);
        }
    }
}
