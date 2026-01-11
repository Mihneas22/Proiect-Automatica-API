using Application.DTOs.Admin.CheckAdmin;
using Application.DTOs.Admin.GetProblems;
using Application.DTOs.Admin.GetUsers;
using Application.Repository;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public async Task<CheckAdminResponse> CheckAdminRepository(CheckAdminDTO checkAdminDTO)
        {
            if (checkAdminDTO == null)
                return new CheckAdminResponse(false, "Invalid data");

            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(checkAdminDTO.Token))
            {
                var jwtToken = handler.ReadJwtToken(checkAdminDTO.Token);
                var name = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;
                var email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;
                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;
                var expires = jwtToken.ValidTo;

                if(expires > DateTime.Now)
                {
                    var admin = await dbContext.AdminEntity!.FirstOrDefaultAsync(ad => ad.Email == email && ad.Username == name);
                    if (admin != null)
                        return new CheckAdminResponse(true, "Admin found!");
                    else
                        return new CheckAdminResponse(false, "Admin not found");

                }

                return new CheckAdminResponse(false, "Tokenul expired");
            }
            else
                return new CheckAdminResponse(false, "User not found");
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
