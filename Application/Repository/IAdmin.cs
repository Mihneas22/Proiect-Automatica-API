using Application.DTOs.Admin.CheckAdmin;
using Application.DTOs.Admin.GetProblems;
using Application.DTOs.Admin.GetUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repository
{
    public interface IAdmin
    {
        Task<CheckAdminResponse> CheckAdminRepository(CheckAdminDTO checkAdminDTO);

        Task<GetProblemsResponse> GetProblemsDataAdmin();

        Task<GetUsersResponse> GetUsersDataAdmin();
    }
}
