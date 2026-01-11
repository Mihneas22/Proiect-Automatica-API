using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Admin.CheckAdmin
{
    public class CheckAdminDTO
    {
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
