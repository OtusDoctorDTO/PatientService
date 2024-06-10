using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Repositories
{
    public interface IPatientOutboxRepository
    {
        Task AddAsync(OutboxMessage message);
    }
}
