using Application.DTOs.Problem.AddProblem;
using Application.DTOs.Problem.DeleteProblem;
using Application.DTOs.Problem.GetProblemById;
using Application.DTOs.Problem.GetProblems;
using Application.Repository;
using Domain.Entities;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infastructure.Repository
{
    public class ProblemRepository : IProblem
    {
        private readonly ApplicationDbContext dbContext;

        public ProblemRepository(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task<AddProblemResponse> AddProblemRepository(AddProblemDTO addProblemDTO)
        {
            if (addProblemDTO == null)
                return new AddProblemResponse(false, "Invalid data");

            var pb = await dbContext.ProblemEntity!.FirstOrDefaultAsync(pb => pb.Name == addProblemDTO.Name);
            if (pb != null)
                return new AddProblemResponse(false, "Problem with name exists");

            dbContext.Add(new Problem
            {
                Name = addProblemDTO.Name,
                Lab = addProblemDTO.Lab,
                Tags = addProblemDTO.Tags,
                Content = addProblemDTO.Content,
                Requests = addProblemDTO.Requests,
                Points = addProblemDTO.Points,
                InputsJson = addProblemDTO.InputsJson,
                OutputsJson = addProblemDTO.OuputsJson,
                AcceptanceRate = 0.0,
                ProblemSubmissions = new List<Submission>(),
                CreatedAt = DateTime.Now
            });

            await dbContext.SaveChangesAsync();

            pb = await dbContext.ProblemEntity!.FirstOrDefaultAsync(pb => pb.Name == addProblemDTO.Name);
            if (pb != null)
                return new AddProblemResponse(true, "Sucess!");
            else
                return new AddProblemResponse(false, "Problem was not added");
        }

        public async Task<DeleteProblemResponse> DeleteProblemRepository(DeleteProblemDTO deleteProblemDTO)
        {
            if (deleteProblemDTO == null)
                return new DeleteProblemResponse(false, "Invalid data");

            var pb = await dbContext.ProblemEntity!
                .Include(pb => pb.ProblemSubmissions)
                .FirstOrDefaultAsync(pb => pb.ProblemId == Guid.Parse(deleteProblemDTO.Id));
            if (pb == null)
                return new DeleteProblemResponse(false, "Problem not found");

            dbContext.ProblemEntity!.Remove(pb);

            await dbContext.SaveChangesAsync();

            return new DeleteProblemResponse(true, "Success!");
        }

        public async Task<GetProblemByIdResponse> GetProblemRepository(GetProblemByIdDTO getProblemByIdDTO)
        {
            if (getProblemByIdDTO == null)
                return new GetProblemByIdResponse(false, "Invalid message");

            var pb = await dbContext.ProblemEntity!
                .Include(pb => pb.ProblemSubmissions)
                .FirstOrDefaultAsync(pb => pb.ProblemId == getProblemByIdDTO.ProblemId);
            if (pb != null)
                return new GetProblemByIdResponse(true, "Success!", pb);
            else
                return new GetProblemByIdResponse(false, "Problem not found");
        }

        public async Task<GetProblemsResponse> GetProblemsRepository()
        {
            var prob = await dbContext.ProblemEntity!.Take(15).ToListAsync();

            if (prob == null)
                return new GetProblemsResponse(false, "Error retrieving");
            else
                return new GetProblemsResponse(true, "Succes", prob);
        }
    }
}
