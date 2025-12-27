using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Problem.GetProblems
{
    public record GetProblemsResponse(bool Flag, string message = null!, List<Domain.Entities.Problem> problems = null!);
}
