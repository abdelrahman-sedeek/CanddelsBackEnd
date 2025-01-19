using Microsoft.AspNetCore.Hosting;

namespace CanddelsBackEnd.Services
{
    public class FileUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> UploadImage(IFormFile image,string folderName)
        {
            if (image == null) return null;

            // Validate Image Type and Size
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
            if (!allowedTypes.Contains(image.ContentType))
            {
                throw new ArgumentException("Invalid file type. Only JPEG, PNG, and JPG are allowed.");
            }

            if (image.Length > 5 * 1024 * 1024) // 5MB size limit
            {
                throw new ArgumentException("File size exceeds 5MB.");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Ensure directory exists
            Directory.CreateDirectory(uploadsFolder);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            // Construct the image URL (relative path)
            return Path.Combine("https://localhost:7012/",$"{folderName}/{uniqueFileName}");
        }
    }
}
