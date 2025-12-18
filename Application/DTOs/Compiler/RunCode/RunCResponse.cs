using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Compiler.RunCode
{
    public record RunCResponse(bool Flag, string message = null!);
}
