using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Problem.GetProblemById
{
    public record GetProblemByIdResponse(bool Flag, string message = null!, Domain.Entities.Problem problem = null!);
}
