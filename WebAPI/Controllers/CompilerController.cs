using Application.DTOs.Compiler.AddSubmission;
using Application.DTOs.Compiler.RunCode;
using Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompilerController : ControllerBase
    {
        private readonly ICompiler _compilerRepo;

        public CompilerController(ICompiler compilerRepo)
        {
            _compilerRepo = compilerRepo;
        }

        [EnableRateLimiting("facult-policy")]
        [Authorize(Roles = "user,admin")]
        [HttpPost("runCode")]
        public async Task<ActionResult<RunCResponse>> CompileAndRunCodeAsync(RunCDTO runCDTO)
        {
            var result = await _compilerRepo.CompileAndRunCode(runCDTO);
            return Ok(result);
        }

        [EnableRateLimiting("facult-policy")]
        [Authorize(Roles = "user,admin")]
        [HttpPost("addSubmission")]
        public async Task<ActionResult<RunCResponse>> AddSubmissionAsync(AddSubmissionDTO addSubmissionDTO)
        {
            var result = await _compilerRepo.AddSubmissionRepository(addSubmissionDTO);
            return Ok(result);
        }
    }
}
