using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Admin.AddAdminResponse
{
    public record AddAdminResponse(bool Flag, string message = null!);
}
