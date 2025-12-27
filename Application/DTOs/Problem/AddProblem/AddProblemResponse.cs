using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Problem.AddProblem
{
    public record AddProblemResponse(bool Flag, string message = null!);
}
