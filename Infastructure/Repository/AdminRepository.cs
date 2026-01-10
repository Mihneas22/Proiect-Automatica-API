using Application.DTOs.Admin.GetProblems;
using Application.DTOs.Admin.GetUsers;
using Application.Repository;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infastructure.Repository
{
    public class AdminRepository : IAdmin
    {
        private readonly ApplicationDbContext dbContext;

        public AdminRepository(ApplicationDbContext applicationDb)
        {
            dbContext = applicationDb;
        }
        public async Task<GetProblemsResponse> GetProblemsDataAdmin()
        {
            var problems = await dbContext.ProblemEntity!
                .Include(pb => pb.ProblemSubmissions)
                .ToListAsync();

            if (problems != null)
                return new GetProblemsResponse(true, "Success!", problems);
            else
                return new GetProblemsResponse(false, "Failed retrieval");
        }

        public async Task<GetUsersResponse> GetUsersDataAdmin()
        {
            var users = await dbContext.UserEntity!
                .Include(us => us.UserSubmissions)
                .ToListAsync();

            if (users != null)
                return new GetUsersResponse(true, "Success!", users);
            else
                return new GetUsersResponse(false, "Failed retrieval");
        }
    }
}
