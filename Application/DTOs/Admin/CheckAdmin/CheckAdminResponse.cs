using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Admin.CheckAdmin
{
    public record CheckAdminResponse(bool Flag, string message = null!);
}
