using Application.DTOs.Admin.AddAdmin;
using Application.DTOs.Admin.AddAdminResponse;
using Application.DTOs.User.GetUser;
using Application.DTOs.User.LoginUser;
using Application.DTOs.User.RegisterUser;
using Application.Repository;
using Domain.Entities;
using Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infastructure.Repository
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext dbContext;
        public IConfiguration configuration;

        public UserRepository(ApplicationDbContext _dbContext, IConfiguration configuration)
        {
            this.dbContext = _dbContext;
            this.configuration = configuration;
        }

        private string GenerateJWTToken(User? user, Admin? admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            if (admin != null)
            {
                var adminClaims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new Claim(ClaimTypes.Name, admin.Username!),
                    new Claim(ClaimTypes.Email, admin.Email!),
                    new Claim(ClaimTypes.Role, "admin")
                };

                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: adminClaims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else if (user != null)
            {
                var userClaims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, "user")
                };


                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: userClaims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
                return string.Empty;
        }
        public async Task<LoginUserResponse> LoginUserRepository(LoginUserDTO loginUserDTO)
        {
            if (loginUserDTO == null)
                return new LoginUserResponse(false, "Invalid data");

            var userF = await dbContext.UserEntity!.FirstOrDefaultAsync(user => user.Username == loginUserDTO.UsernameOrEmail || user.Email == loginUserDTO.UsernameOrEmail);
            var adminF = await dbContext.AdminEntity!.FirstOrDefaultAsync(admin => admin.Username == loginUserDTO.UsernameOrEmail || admin.Email == loginUserDTO.UsernameOrEmail);

            if(userF != null)
            {
                bool checkPass = BCrypt.Net.BCrypt.Verify(loginUserDTO.Password,userF.Password);
                if (checkPass)
                {
                    string token = GenerateJWTToken(userF,null!);
                    return new LoginUserResponse(true, "Succesfull login!", token);
                }
                else
                    return new LoginUserResponse(false, "Invalid email or password.");
            }
            else if(adminF != null)
            {
                bool checkPass = BCrypt.Net.BCrypt.Verify(loginUserDTO.Password,adminF.Password);
                if (checkPass)
                {
                    string token = GenerateJWTToken(null!,adminF);
                    return new LoginUserResponse(true, "Succesfull login! admin", token);
                }
                else
                    return new LoginUserResponse(false, "Invalid email or password.");
            }
            else
                return new LoginUserResponse(false, "No user found");
        }

        public async Task<RegisterUserResponse> RegisterUserRepository(RegisterUserDTO registerUserDTO)
        {
            if (registerUserDTO == null)
                return new RegisterUserResponse(false, "Invalid data.");

            if (registerUserDTO.Password != registerUserDTO.ConfirmPassword)
                return new RegisterUserResponse(false, "Passwords do not match.");

            var user = await dbContext.UserEntity!.FirstOrDefaultAsync(u => u.Email == registerUserDTO.Email || u.Username == registerUserDTO.Username);
            var admin = await dbContext.AdminEntity!.FirstOrDefaultAsync(u => u.Email == registerUserDTO.Email || u.Username == registerUserDTO.Username);
            if (user != null || admin != null)
                return new RegisterUserResponse(false, "User with email or username already exists.");

            dbContext.UserEntity!.Add(new User
            {
                Username = registerUserDTO.Username,
                Email = registerUserDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password),
                UserSubmissions = new List<Submission>(),
                CreatedAt = DateTime.Now
            });

            await dbContext.SaveChangesAsync();

            return new RegisterUserResponse(true, "Success!");
        }

        public async Task<GetUserResponse> GetUserRepository(GetUserDTO getUserDTO)
        {
            if (getUserDTO == null)
                return new GetUserResponse(false, "Invalid data");

            var user = await dbContext.UserEntity!
                .Include(sub => sub.UserSubmissions)
                .FirstOrDefaultAsync(us => us.Email == getUserDTO.Email && us.Username == getUserDTO.Username);
            if(user == null)
                return new GetUserResponse(false, "Invalid data");
            else
                return new GetUserResponse(true, "User found!",user);
        }

        public async Task<AddAdminResponse> AddAdminRepository(AddAdminDTO addAdminDTO)
        {
            if (addAdminDTO == null)
                return new AddAdminResponse(false, "Invalid data.");

            var user = await dbContext.UserEntity!.FirstOrDefaultAsync(u => u.Email == addAdminDTO.Email || u.Username == addAdminDTO.Username);
            var admin = await dbContext.AdminEntity!.FirstOrDefaultAsync(u => u.Email == addAdminDTO.Email || u.Username == addAdminDTO.Username);
            if (user != null || admin != null)
                return new AddAdminResponse(false, "User with email or username already exists.");

            dbContext.AdminEntity!.Add(new Admin
            {
                Username = addAdminDTO.Username,
                Email = addAdminDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(addAdminDTO.Password),
                Logs = new List<string>(),
                CreatedAt = DateTime.Now
            });

            await dbContext.SaveChangesAsync();

            return new AddAdminResponse(true, "Success!");
        }
    }
}
