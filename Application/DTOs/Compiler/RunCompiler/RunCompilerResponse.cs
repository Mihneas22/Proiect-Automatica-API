using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Compiler.RunCompiler
{
    public record RunCompilerResponse(bool Flag, string message = null!);
}
