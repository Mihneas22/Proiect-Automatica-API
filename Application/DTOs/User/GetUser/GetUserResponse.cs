using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.User.GetUser
{
    public record GetUserResponse(bool Flag, string message = null!, Domain.Entities.User user = null!);
}
