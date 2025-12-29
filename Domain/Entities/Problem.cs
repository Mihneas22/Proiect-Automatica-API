using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json;

namespace Domain.Entities
{
    public class Problem
    {
        [Key]
        public Guid ProblemId { get; set; }

        public string? Name { get; set; }

        public string? Lab { get; set; }

        public ICollection<string>? Tags { get; set; }

        public string? Content { get; set; }

        public ICollection<string>? Requests { get; set; }

        public int? Points { get; set; }

        public List<string>? InputsJson { get; set; }

        public List<string>? OutputsJson { get; set; }

        public double? AcceptanceRate { get; set; }

        public ICollection<Submission> ProblemSubmissions { get; set; } = new List<Submission>();

        public DateTime? CreatedAt { get; set; }
    }
}
