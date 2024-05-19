using DAL;
using DAL.Models;
using QLBH.Common.BLL;
using QLBH.Common.Rsp;
using Common.DAL;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using System;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
namespace BLL
{
    public class UserService : GenericSvc<UserRepository, User>
    {
        private UserRepository userRepository = new UserRepository();
        private readonly Cloudinary _cloudinary;

        public UserService(IConfiguration configuration)
        {
            // CLOUDINARY_URL=cloudinary://<API_KEY>:<API_SECRET>@<CLOUD_NAME>
            var cloudinaryUrl = "cloudinary://922611133231776:Q0bJhJc_3Z06xk1mFMf0oDSgWxo@dwvg5xlum";
            _cloudinary = new Cloudinary(cloudinaryUrl);
            _cloudinary.Api.Secure = true;
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt = new byte[16]);
            }
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combine the salt and password bytes for later use
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert to base64
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }

        public async Task<SingleRsp> CreateUserAsync(UserReq userReq)
        {
            var res = new SingleRsp();

            User newUser  = new User {

                Id = Guid.NewGuid(),
                FirstName = userReq.FirstName,
                LastName = userReq.LastName,
                Email = userReq.Email,
                Username = userReq.Username,
                Password = HashPassword(userReq.Password),
            };
            

            Userinfo userinfo = new Userinfo
            {
                UserId = newUser.Id,
            };
           


            // Upload avatar to Cloudinary
            if (userReq.Avatar is not null)
            {
                using var memoryStream = new MemoryStream();
                await userReq.Avatar.CopyToAsync(memoryStream);

                string avatarUrl = await UploadAvatarAsync(userReq.Avatar);
                newUser.Avatar = avatarUrl;
            }

            res = await userRepository.AddUserAsync(newUser, userinfo);
            return res;
        }

        private async Task<string> UploadAvatarAsync(IFormFile file)
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
                //,
                //UseFilename = true,
                //UniqueFilename = false,
                //Overwrite = true
            };

            ImageUploadResult uploadResult = null;

            try
            {
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            catch (Exception ex)
            {
                var cc = ex;
            }
            finally
            {
                File.Delete(tempFileName);
            }

            return uploadResult?.SecureUrl?.ToString();
        }
    }
}
