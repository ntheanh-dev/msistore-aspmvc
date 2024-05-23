﻿using AutoMapper;
using BLL;
using BLL.DTOs;
using Common.Req.UserRequest;
using Common.Rsp;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MSISTORE.WEB.Controllers
{
    public class UserController : Controller
    {
       private readonly UserService _userService;

        public UserController(IConfiguration configuration, IMapper mapper) {
            _userService = new UserService(configuration, mapper);
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromForm] UserReq userReq)
        {
            return Ok(await _userService.CreateUserAsync(userReq));
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginReq loginReq)
        {
            var res = await _userService.AuthenticateJWTAsync(loginReq);
            return Ok(res.Resutls);
        }
        [HttpGet("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByUsernameAsync(username); // Sử dụng phương thức bất đồng bộ
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserRsp { 
                Id = user.Id,
                Fristname = user.FirstName,
                Lastname = user.LastName,
                Username = user.Username,
                Email = user.Email,
                avatar = user.Avatar,
            };
            return Ok(userDto);
        }
        [HttpPatch("{userId:long}/update-user")]
        public async Task<IActionResult> UpdateUser(long userId, [FromForm] UpdateUserReq updateReq)
        {
            var res = await _userService.UpdateUserAsync(userId, updateReq);

            if (res.Success)
            {
                return Ok(res.Resutls);
            }

            return BadRequest(res.Message);
        }
    }
}
