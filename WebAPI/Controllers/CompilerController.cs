using Application.DTOs.Compiler.RunCode;
using Application.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("addSub")]
        public async Task<ActionResult<RunCResponse>> CompileAndRunCode(RunCDTO runCDTO)
        {
            var result = await _compilerRepo.CompileAndRunCode(runCDTO);
            return Ok(result);
        }
    }
}
