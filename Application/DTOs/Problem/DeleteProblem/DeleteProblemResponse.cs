using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Problem.DeleteProblem
{
    public record DeleteProblemResponse(bool Flag, string message = null!);
}
