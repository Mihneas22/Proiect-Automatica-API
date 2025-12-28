using Application.DTOs.Compiler.RunCode;
using Application.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Infastructure.Repository
{
    public class CompilerRepository : ICompiler
    {
        public async Task<RunCResponse> CompileAndRunCode(RunCDTO runCDTO)
        {
            var submissionId = Guid.NewGuid();
            var workDir = Path.Combine("D:\\facultate\\ProjetFacult\\Temp\\submissions", submissionId.ToString());
            Directory.CreateDirectory(workDir);


            var runScriptSource = Path.Combine("D:\\facultate\\ProjetFacult\\CodeRunner\\cpp", "run.sh");
            var runScriptDest = Path.Combine(workDir, "run.sh");
            File.Copy(runScriptSource, runScriptDest, true);


            foreach(var file in runCDTO.NamesOfFiles)
            {
                if (runCDTO.SourceCode[file] != null)
                {
                    File.WriteAllText(Path.Combine(workDir, file), runCDTO.SourceCode[file]);
                }
            }
            File.WriteAllText(Path.Combine(workDir, "input.txt"), runCDTO.Input);


            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"run --rm -v \"{workDir}:/app\" cpp-runner bash /app/run.sh",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };
            process.Start();


            if (!process.WaitForExit(2000))
            {
                process.Kill();
                return new RunCResponse(false,"Exceed time limit");
            }

            var compileErrorPath = Path.Combine(workDir, "compile_error.txt");
            if (File.Exists(compileErrorPath))
            {
                var compileError = File.ReadAllText(compileErrorPath);
                if (!string.IsNullOrWhiteSpace(compileError))
                {
                    return new RunCResponse(false, $"Compilation Error:\n{compileError}");
                }
            }


            var outputPath = Path.Combine(workDir, "output.txt");
            if (!File.Exists(outputPath))
            {
                return new RunCResponse(false, "Runtime error: output.txt was not generated");
            }

            var output = File.ReadAllText($"{workDir}/output.txt");

            Directory.Delete(workDir, recursive: true);
            return new RunCResponse(true, $"{output.ToString()}");
        }
    }
}
