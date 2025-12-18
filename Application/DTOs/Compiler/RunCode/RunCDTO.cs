using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Compiler.RunCode
{
    public class RunCDTO
    {
        [Required]
        public string SourceCode { get; set; } = string.Empty;

        [Required]
        public string Input { get; set; } = string.Empty;
    }
}
