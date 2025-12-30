using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities.Middlewares
{
    public class IdempotencyRecord
    {
        public Guid? Id { get; set; }

        [Key]
        public string Key { get; set; } = null!;

        public int StatusCode { get; set; }

        public string ResponseBody { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
