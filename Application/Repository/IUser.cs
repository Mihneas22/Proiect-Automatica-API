using Application.DTOs.Admin.AddAdmin;
using Application.DTOs.Admin.AddAdminResponse;
using Application.DTOs.User.GetUser;
using Application.DTOs.User.LoginUser;
using Application.DTOs.User.RegisterUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repository
{
    public interface IUser
    {
        Task<RegisterUserResponse> RegisterUserRepository(RegisterUserDTO registerUserDTO);
        
        Task<AddAdminResponse> AddAdminRepository(AddAdminDTO addAdminDTO);

        Task<LoginUserResponse> LoginUserRepository(LoginUserDTO loginUserDTO);

        Task<GetUserResponse> GetUserRepository(GetUserDTO getUserDTO);
    }
}
