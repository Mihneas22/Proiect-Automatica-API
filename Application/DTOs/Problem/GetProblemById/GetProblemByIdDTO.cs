using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Problem.GetProblemById
{
    public class GetProblemByIdDTO
    {
        [Required]
        public Guid ProblemId { get; set; } = Guid.Empty;
    }
}
