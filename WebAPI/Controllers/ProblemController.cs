using Application.DTOs.Problem.AddProblem;
using Application.DTOs.Problem.GetProblemById;
using Application.DTOs.Problem.GetProblems;
using Application.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly IProblem problemRepo;

        public ProblemController(IProblem problemRepo)
        {
            this.problemRepo = problemRepo;
        }

        [HttpPost("addProblem")]
        public async Task<ActionResult<AddProblemResponse>> AddProblemAsync(AddProblemDTO addProblemDTO)
        {
            var result = await problemRepo.AddProblemRepository(addProblemDTO);
            return Ok(result);
        }

        [HttpGet("getProblem/{id}")]
        public async Task<ActionResult<GetProblemByIdResponse>> GetProblemByIdAsync(string id)
        {
            var result = await problemRepo.GetProblemRepository(new GetProblemByIdDTO { ProblemId = Guid.Parse(id) });
            return Ok(result);
        }

        [HttpGet("getProblems")]
        public async Task<ActionResult<GetProblemsResponse>> GetProblemsAsync()
        {
            var result = await problemRepo.GetProblemsRepository();
            return Ok(result);
        }

    }
}
