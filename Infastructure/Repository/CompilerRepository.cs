using Application.DTOs.Compiler.AddSubmission;
using Application.DTOs.Compiler.RunCode;
using Application.DTOs.Compiler.RunCompiler;
using Application.Repository;
using Domain.Entities;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Infastructure.Repository
{
    public class CompilerRepository : ICompiler
    {
        private readonly ApplicationDbContext dbContext;

        public CompilerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private void UpdateAcceptanceRate(Domain.Entities.Problem pb,bool currentIsCorrect)
        {
            int correct = pb.ProblemSubmissions.Count(s => s.Status == Domain.Entities.Submission.StatusType.Approved);
            if (currentIsCorrect) correct++;

            int total = pb.ProblemSubmissions.Count + 1;
            pb.AcceptanceRate = Math.Round((double)correct * 100.0 / total, 2);
        }

        private RunCResponse CompileCode(RunCompilerDTO runCDTO)
        {
            var submissionId = Guid.NewGuid();
            //laptop - C:\\Users\\pc\\coding\\api_fac\\Proiect-Automatica-API\\Temp\\submissions\\
            //pc - D:\\\\facultate\\\\ProjetFacult\\\\Temp\\\\submissions
            var workDir = Path.Combine("D:\\\\facultate\\\\ProjetFacult\\\\Temp\\\\submissions", submissionId.ToString());
            Directory.CreateDirectory(workDir);

            //laptop - C:\\Users\\pc\\coding\\api_fac\\Proiect-Automatica-API\\CodeRunner\\cpp\\
            //pc - D:\\facultate\\ProjetFacult\\CodeRunner\\cpp
            var runScriptSource = Path.Combine("D:\\facultate\\ProjetFacult\\CodeRunner\\cpp", "run.sh");
            var runScriptDest = Path.Combine(workDir, "run.sh");
            File.Copy(runScriptSource, runScriptDest, true);


            foreach (var file in runCDTO.NamesOfFiles)
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


            if (!process.WaitForExit(5000))
            {
                var error = process.StandardError.ReadToEnd();
                process.Kill();
                return new RunCResponse(false, $"Timeout. Error: {error}");
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

        public async Task<AddSubmissionResponse> AddSubmissionRepository(AddSubmissionDTO addSubDTO)
        {
            if (addSubDTO == null)
                return new AddSubmissionResponse(false, "Invalid data");

            var pb = await dbContext.ProblemEntity!
                .Include(sub => sub.ProblemSubmissions)
                .FirstOrDefaultAsync(pb => pb.ProblemId == addSubDTO.ProblemId);
            var user = await dbContext.UserEntity!.FirstOrDefaultAsync(us => us.Id == addSubDTO.UserId);
            if (pb == null || user == null)
                return new AddSubmissionResponse(false, "Problem or user not found");

            var sub1 = await dbContext.SubmissionEntity!.FirstOrDefaultAsync(sub => sub.ProblemId == addSubDTO.ProblemId && sub.UserId == addSubDTO.UserId);
            if(sub1 != null)
                if(sub1.Status == Domain.Entities.Submission.StatusType.Approved)
                    return new AddSubmissionResponse(false, "Submission already sent");

            int indx = 0;

            int correct = 0;
            int nrt = 0;

            foreach (var input in pb.InputsJson!)
            {
                var runTest = CompileCode(new RunCompilerDTO
                {
                    SourceCode = addSubDTO.SourceCode,
                    NamesOfFiles = addSubDTO.NamesOfFiles,
                    Input = input,
                });

                if (runTest.Flag == false)
                {
                    dbContext.SubmissionEntity!.Add(new Domain.Entities.Submission
                    {
                        UserId = addSubDTO.UserId,
                        User = user,
                        ProblemId = addSubDTO.ProblemId,
                        Problem = pb,
                        Content = addSubDTO.SourceCode.ToString(),
                        Status = Domain.Entities.Submission.StatusType.Rejected,
                        Message = $"Compilation error at test {indx+1}",
                        SubmittedAt = DateTime.Now
                    });

                    await dbContext.SaveChangesAsync();
                    return new AddSubmissionResponse(false, $"Error found in test number {indx + 1}: {runTest.message}");
                }
                    


                if (runTest.message != pb.OutputsJson![indx])
                {
                    dbContext.SubmissionEntity!.Add(new Domain.Entities.Submission
                    {
                        UserId = addSubDTO.UserId,
                        User = user,
                        ProblemId = addSubDTO.ProblemId,
                        Problem = pb,
                        Content = "empty content",
                        Status = Domain.Entities.Submission.StatusType.Rejected,
                        Message = $"Test failed: {indx + 1}",
                        SubmittedAt = DateTime.Now
                    });

                    UpdateAcceptanceRate(pb, false);
                    await dbContext.SaveChangesAsync();
                    return new AddSubmissionResponse(false, $"Expected output {pb.OutputsJson![indx]} in test {indx + 1} but got {runTest.message}");
                }   

                indx += 1;
            }

            string contentCode = "";
            foreach (var file in addSubDTO.SourceCode)
            {
                contentCode += file.Key + "\n";
                contentCode += file.Value + "\n";
            }

            var sub = new Domain.Entities.Submission
            {
                UserId = addSubDTO.UserId,
                User = user,
                ProblemId = addSubDTO.ProblemId,
                Problem = pb,
                Content = contentCode,
                Status = Domain.Entities.Submission.StatusType.Approved,
                Message = "Added succsefully!",
                SubmittedAt = DateTime.Now
            };

            if (dbContext.SubmissionEntity == null)
            {
                throw new InvalidOperationException("SubmissionEntity DbSet is not initialized.");
            }

            UpdateAcceptanceRate(pb, true);

            dbContext.SubmissionEntity.Add(sub);

            user.UserSubmissions.Add(sub);
            pb.ProblemSubmissions.Add(sub);

            await dbContext.SaveChangesAsync();

            return new AddSubmissionResponse(true, "Added sucsefully!");
        }

        public async Task<RunCResponse> CompileAndRunCode(RunCDTO runCDTO)
        {
            // LOG PENTRU DEBUG:
            Console.WriteLine($"DEBUG: ProblemId primit: {runCDTO.ProblemId}");

            if (runCDTO == null)
                return new RunCResponse(false, "Invalid data");

            var pb = await dbContext.ProblemEntity!.FirstOrDefaultAsync(pb => pb.ProblemId == runCDTO.ProblemId);
            if (runCDTO.UserId != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                var user = await dbContext.UserEntity!.FirstOrDefaultAsync(us => us.Id == runCDTO.UserId);
                if (pb == null || user == null)
                    return new RunCResponse(false, "User not found");
            }

            if (pb == null)
                return new RunCResponse(false, "Problem not found");

            int indx = 0;
            foreach (var input in pb.InputsJson!)
            {
                var runTest = CompileCode(new RunCompilerDTO
                {
                    SourceCode = runCDTO.SourceCode,
                    NamesOfFiles = runCDTO.NamesOfFiles,
                    Input = input
                });

                if (runTest.Flag == false)
                    return new RunCResponse(false, $"Error found in test number {indx + 1}: ${runTest.message}");

                if (runTest.message != pb.OutputsJson![indx])
                    return new RunCResponse(false, $"Expected output {pb.OutputsJson![indx]} in test {indx + 1} but got {runTest.message}");

                indx += 1;
            }

            return new RunCResponse(true, "All tests passed!");
        }
    }
}
