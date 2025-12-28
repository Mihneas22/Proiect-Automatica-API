using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Domain.Entities.Submission;

namespace Application.DTOs.Compiler.AddSubmission
{
    public class AddSubmissionDTO
    {
        [Required]
        public Guid UserId { get; set; } = Guid.Empty;

        [Required]
        public Guid ProblemId { get; set; } = Guid.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;
    }
}
