using Application.DTOs.Compiler.AddSubmission;
using Application.DTOs.Compiler.RunCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repository
{
    public interface ICompiler
    {
        Task<RunCResponse> CompileAndRunCode(RunCDTO runCDTO);

        Task<AddSubmissionResponse> AddSubmissionRepository(AddSubmissionDTO addSubDTO);
    }
}
