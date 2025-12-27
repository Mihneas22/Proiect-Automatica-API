using Application.DTOs.User.LoginUser;
using Application.DTOs.User.RegisterUser;
using Application.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("registerUser")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var result = await userRepo.RegisterUserRepository(registerUserDTO);
            return Ok(result);
        }

        [HttpPost("loginUser")]
        public async Task<ActionResult<LoginUserResponse>> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            var result = await userRepo.LoginUserRepository(loginUserDTO);
            return Ok(result);
        }
    }
}
