using Application.DTOs.Admin.AddAdmin;
using Application.DTOs.Admin.AddAdminResponse;
using Application.DTOs.User.GetUser;
using Application.DTOs.User.LoginUser;
using Application.DTOs.User.RegisterUser;
using Application.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUser userRepo;

        public AdminController(IUser _userRepo)
        {
            this.userRepo = _userRepo;
        }

        [EnableRateLimiting("facult-policy")]
        [HttpPost("registerAdmin")]
        public async Task<ActionResult<AddAdminResponse>> AddAdminAsync(AddAdminDTO addAdminDTO)
        {
            var result = await userRepo.AddAdminRepository(addAdminDTO);
            return Ok(result);
        }
    }
}
