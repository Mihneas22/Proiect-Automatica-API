using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Problem.AddProblem
{
    public class AddProblemDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string? Lab { get; set; } = string.Empty;

        [Required]
        public ICollection<string> Tags { get; set; } = new List<string>();

        [Required]
        public string? Content { get; set; } = string.Empty;

        [Required]
        public ICollection<string> Requests { get; set; } = new List<string>();

        [Required]
        public int Points { get; set; } = 0;

        [Required]
        public List<string> InputsJson { get; set; } = new List<string>();

        [Required]
        public List<string> OuputsJson { get; set; } = new List<string>();
    }
}
