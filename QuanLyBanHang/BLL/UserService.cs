using AutoMapper;
using BLL.DTOs;
using BLL.Helper;
using BLL.Hepler;
using BLL.Token;
using Common.Req.UserRequest;
using Common.Rsp;
using DAL;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using QLBH.Common.BLL;
using QLBH.Common.Rsp;

namespace BLL
{
    public class UserService : GenericSvc<UserRepository, User>
    {
        private readonly UserRepository _userRepository;
        private readonly HelperCloudinary _helperCloudinary;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, IMapper mapper)
        {
            // CLOUDINARY_URL=cloudinary://<API_KEY>:<API_SECRET>@<CLOUD_NAME>
            var cloudinaryUrl = "cloudinary://922611133231776:Q0bJhJc_3Z06xk1mFMf0oDSgWxo@dwvg5xlum";
            _helperCloudinary = new HelperCloudinary(cloudinaryUrl);
            _mapper = mapper;
            _userRepository = new UserRepository();
            this._configuration = configuration;
        }


        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var users = await _userRepository.FindAsync(u => u.Username == username);
            return users.FirstOrDefault();
        }


        public async Task<UserDTO> CreateUserAsync(UserReq userReq)
        {

            User newUser = new User
            {
                FirstName = userReq.FirstName,
                LastName = userReq.LastName,
                Email = userReq.Email,
                Username = userReq.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userReq.Password),
                RoleId = userReq.RoleId
            };
            // Upload avatar to Cloudinary
            if (userReq.Avatar is not null)
            {

                string avatarUrl = await _helperCloudinary.UploadAvatarAsync(userReq.Avatar);
                newUser.Avatar = avatarUrl;

            }
            Userinfo userinfo = new Userinfo
            {
                UserId = newUser.Id
            };
            var result = await _userRepository.AddUserAsync(newUser, userinfo);
            if (!result.Success)
            {
                // Handle error
                throw new Exception("Failed to add userinfo");
            }


            return _mapper.Map<UserDTO>(newUser);
        }



        public async Task<SingleRsp> AuthenticateJWTAsync(LoginReq loginReq)
        {
            var res = new SingleRsp();

            var users = await _userRepository.FindAsync(u => u.Username == loginReq.Username);
            var user = users.FirstOrDefault();

            
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginReq.Password, user.Password))
            {
                res.SetError("Invalid credentials.");
                return res;
            }
            var tokenService = new TokenService(_configuration);
            var (accessToken, refreshToken) = tokenService.GenerateJwtToken(user);
            res.Resutls = new
            {
                //
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return res;
        }



        public async Task<SingleRsp> UpdateUserAsync(long userId, UpdateUserReq updateUserReq)
        {
            var res = new SingleRsp();

            // Lấy thông tin user hiện tại từ database
            var existingUser = await _userRepository.GetUserByIdAsync(userId);

            if (existingUser == null)
            {
                res.SetError("User not found.");
                return res;
            }

            // Cập nhật thông tin user từ UpdateUserReq
            existingUser.FirstName = updateUserReq.FirstName ?? existingUser.FirstName;
            existingUser.LastName = updateUserReq.LastName ?? existingUser.LastName;
            existingUser.Email = updateUserReq.Email ?? existingUser.Email;

            // Upload avatar lên Cloudinary nếu có
            if (updateUserReq.Avatar != null)
            {
                string avatarUrl = await _helperCloudinary.UploadAvatarAsync(updateUserReq.Avatar);
                existingUser.Avatar = avatarUrl;
            }

            // Cập nhật thông tin userinfo
            if (existingUser.Userinfo == null)
            {
                existingUser.Userinfo = new Userinfo { UserId = existingUser.Id };
            }

            var userinfo = existingUser.Userinfo;

            userinfo.Country = updateUserReq.Country ?? userinfo.Country;
            userinfo.City = updateUserReq.City ?? userinfo.City;
            userinfo.Street = updateUserReq.Street ?? userinfo.Street;
            userinfo.HomeNumber = updateUserReq.HomeNumber ?? userinfo.HomeNumber;
            userinfo.PhoneNumber = updateUserReq.PhoneNumber ?? userinfo.PhoneNumber;

            
            // Gọi phương thức UpdateAsync của UserRepository
            var updateRes = await _userRepository.UpdateAsync(existingUser);

        
            if (updateRes.Success)
            {
                res.Resutls = updateUserReq;
            }
            else
            {
                res.SetError(updateRes.Message);
            }

            return res;
        }




    }


}
