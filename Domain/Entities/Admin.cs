using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Admin
    {
        [Key]
        public Guid Id { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public List<string>? Logs { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
