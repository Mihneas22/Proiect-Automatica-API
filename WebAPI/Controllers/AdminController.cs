using Application.DTOs.Admin.AddAdmin;
using Application.DTOs.Admin.AddAdminResponse;
using Application.DTOs.Admin.CheckAdmin;
using Application.DTOs.Admin.GetProblems;
using Application.DTOs.Admin.GetUsers;
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
    public class AdminController : ControllerBase
    {
        private readonly IUser userRepo;
        private readonly IAdmin adminRepo;

        public AdminController(IUser _userRepo, IAdmin _adminRepo)
        {
            this.userRepo = _userRepo;
            this.adminRepo = _adminRepo;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("registerAdmin")]
        public async Task<ActionResult<AddAdminResponse>> AddAdminAsync(AddAdminDTO addAdminDTO)
        {
            var result = await userRepo.AddAdminRepository(addAdminDTO);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getProblems")]
        public async Task<ActionResult<GetProblemsResponse>> GetProblemsAdminAsync()
        {
            var result = await adminRepo.GetProblemsDataAdmin();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getUsers")]
        public async Task<ActionResult<GetUsersResponse>> GetUsersAdminAsync()
        {
            var result = await adminRepo.GetUsersDataAdmin();
            return Ok(result);
        }

        [HttpPost("checkAdmin")]
        public async Task<ActionResult<CheckAdminResponse>> CheckAdminAsync(CheckAdminDTO checkAdminDTO)
        {
            var result = await adminRepo.CheckAdminRepository(checkAdminDTO);
            return Ok(result);
        }
    }
}
