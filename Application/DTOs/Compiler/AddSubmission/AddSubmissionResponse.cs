using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Compiler.AddSubmission
{
    public record AddSubmissionResponse(bool Flag, string message = null!);
}
