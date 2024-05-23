using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BLL.Helper
{
    public class HelperCloudinary
    {
        private readonly Cloudinary _cloudinary;

        public HelperCloudinary(string cloudinaryUrl)
        {
            _cloudinary = new Cloudinary(cloudinaryUrl);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadAvatarAsync(IFormFile file)
        {
            var tempFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using (var fileStream = File.Create(tempFileName))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(tempFileName),
                Timestamp = DateTime.Now
            };

            ImageUploadResult uploadResult = null;

            try
            {
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            finally
            {
                File.Delete(tempFileName);
            }

            return uploadResult?.SecureUrl?.ToString();
        }
    }
}
