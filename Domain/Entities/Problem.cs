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

        public string? TestsJson { get; set; }

        [NotMapped]
        public ICollection<ProblemTest> Tests
        {
            get => string.IsNullOrEmpty(TestsJson)
                ? new List<ProblemTest>()
                : JsonSerializer.Deserialize<ICollection<ProblemTest>>(TestsJson)!;

            set => TestsJson = JsonSerializer.Serialize(value);
        }

        public double? AcceptanceRate { get; set; }

        public ICollection<Submission>? ProblemSubmissions { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
