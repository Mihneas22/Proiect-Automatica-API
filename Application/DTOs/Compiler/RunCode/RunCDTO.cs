using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Compiler.RunCode
{
    public class RunCDTO
    {
        [Required]
        public Dictionary<string,string> SourceCode { get; set; } = new Dictionary<string, string>();

        [Required]
        public List<string> NamesOfFiles { get; set; } = new List<string>();

        [Required]
        public Guid UserId { get; set; } = Guid.Empty;

        [Required]
        public Guid ProblemId { get; set; } = Guid.Empty;
    }
}
