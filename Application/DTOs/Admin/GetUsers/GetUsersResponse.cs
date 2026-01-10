using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Admin.GetUsers
{
    public record GetUsersResponse(bool Flag, string message = null!, List<Domain.Entities.User> users = null!);
}
