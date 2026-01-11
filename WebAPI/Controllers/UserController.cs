using Application.DTOs.User.GetUser;
using Application.DTOs.User.LoginUser;
using Application.DTOs.User.RegisterUser;
using Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser userRepo;

        public UserController(IUser _userRepo)
        {
            this.userRepo = _userRepo;
        }

        [EnableRateLimiting("facult-policy")]
        [HttpPost("registerUser")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var result = await userRepo.RegisterUserRepository(registerUserDTO);
            return Ok(result);
        }

        [EnableRateLimiting("facult-policy")]
        [HttpPost("loginUser")]
        public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            var result = await userRepo.LoginUserRepository(loginUserDTO);
            return Ok(result);
        }

        [Authorize(Roles = "user,admin")]
        [HttpPost("getUser")]
        public async Task<ActionResult<GetUserResponse>> GetUserAsync(GetUserDTO getUserDTO)
        {
            var result = await userRepo.GetUserRepository(getUserDTO);
            return Ok(result);
        }
    }
}
