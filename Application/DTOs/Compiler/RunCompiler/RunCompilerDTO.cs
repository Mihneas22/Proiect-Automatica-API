using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Compiler.RunCompiler
{
    public class RunCompilerDTO
    {
        [Required]
        public Dictionary<string, string> SourceCode { get; set; } = new Dictionary<string, string>();

        [Required]
        public List<string> NamesOfFiles { get; set; } = new List<string>();

        [Required]
        public string Input { get; set; } = string.Empty;
    }
}
