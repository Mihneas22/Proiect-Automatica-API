using Application.DTOs.Problem.AddProblem;
using Application.DTOs.Problem.GetProblemById;
using Application.DTOs.Problem.GetProblems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repository
{
    public interface IProblem
    {
        Task<GetProblemByIdResponse> GetProblemRepository(GetProblemByIdDTO getProblemByIdDTO);

        Task<AddProblemResponse> AddProblemRepository(AddProblemDTO addProblemDTO);

        Task<GetProblemsResponse> GetProblemsRepository();
    }
}
