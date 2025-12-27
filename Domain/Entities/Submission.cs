using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Submission
    {
        [Key]
        public Guid SubmissionId { get; set; }

        public Guid? UserId { get; set; }

        public User? User { get; set; }

        public Guid ProblemId { get; set; }

        public Problem? Problem { get; set; }

        public string? Content { get; set; }

        public StatusType? Status { get; set; }

        public string? Message { get; set; }
        public DateTime? SubmittedAt { get; set; }

        public enum StatusType
        {
            Pending,
            Approved,
            Rejected
        }
    }
}
