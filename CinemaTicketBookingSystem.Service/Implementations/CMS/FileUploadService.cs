
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProject.Core.Resources;


namespace CinemaTicketBookingSystem.Service.Implementations.CMS
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _env;
        private readonly long _maxFileSize = 5 * 1024 * 1024; //5MB
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly string _imagesFolder = "uploads";

        public FileUploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<byte[]> GetFileBytesAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<byte[]> GetFileBytesAsync(string base64String)
        {
            // Simulate async operation, if needed
            return await Task.Run(() => Convert.FromBase64String(base64String));
        }

        public async Task<string> UploadFileAsync(IFormFile file , string featureFolder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException(ValidationResources.Invalidfile);

            if (file.Length > _maxFileSize)
                throw new ArgumentException(string.Format(ValidationResources.FileSizeLimit, _maxFileSize / 1024 / 1024));


            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                throw new ArgumentException($"{ValidationResources.InvalidFormat} {string.Join(", ", _allowedExtensions)}");


            var uploadsFolder = Path.Combine(_env.WebRootPath, _imagesFolder , featureFolder);
            Directory.CreateDirectory(uploadsFolder);

            // convert file to byte array   
            var fileBytes = await GetFileBytesAsync(file);

            //   WebP  100%
            var imageProcessor = new ImageProcessingService();
            var processedImage = imageProcessor.ConvertToWebP(fileBytes, quality: 100);

            var uniqueFileName = $" {Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            //var result = _imagesFolder +"/"+ featureFolder + "/" + uniqueFileName;
            return uniqueFileName;
        }

        public async Task<string> UploadFileAsync(byte[] fileBytes, string folderName, string? oldFileName = null)
        {
            // Create the uploads folder if it doesn't exist
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", folderName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (!string.IsNullOrEmpty(oldFileName))
            {
                var oldFilePath = Path.Combine(uploadsFolder, oldFileName);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            //   WebP  100%
            var imageProcessor = new ImageProcessingService();
            var processedImage = imageProcessor.ConvertToWebP(fileBytes, quality: 100);

            // Generate a new GUID and append the original file extension
            var uniqueFileName = $"{Guid.NewGuid()}.webp";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            int index = filePath.IndexOf("uploads");
            string relativePath = filePath.Substring(index);

            // Write the file bytes to the specified path
            await File.WriteAllBytesAsync(filePath, processedImage);

            return uniqueFileName;
        }

        public bool IsValidFile(IFormFile file)
        {
            return ValidateFile(file).isValid;
        }

        public bool IsValidFile(string base64File, string fileName)
        {
            // Implementation for base64 string validation
            throw new NotImplementedException();
        }

        public (bool isValid, string errorMessage) ValidateFile(IFormFile file)
        {
            if (file == null)
                return (false, "File is null.");

            if (!file.ContentType.StartsWith("image/"))
                return (false, "Invalid file type. Only images are allowed.");

            return (true, string.Empty);
        }

        public (bool isValid, string errorMessage) ValidateFile(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) return (false, "File is null.");

            Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
            return (Convert.TryFromBase64String(base64String, buffer, out _), "File is not valid");
        }

        public Task<string> UploadFileAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

   
    }
}
