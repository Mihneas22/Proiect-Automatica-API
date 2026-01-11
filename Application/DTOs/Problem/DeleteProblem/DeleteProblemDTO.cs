using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Problem.DeleteProblem
{
    public class DeleteProblemDTO
    {
        [Required]
        public string Id { get; set; } = string.Empty;
    }
}
