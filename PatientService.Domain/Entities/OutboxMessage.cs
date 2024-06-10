using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Entities
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public string? Destination { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
