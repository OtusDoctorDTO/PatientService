using Microsoft.EntityFrameworkCore;
using PatientService.Data.Context;
using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Repositories
{
    public class PatientOutboxRepository : IPatientOutboxRepository
    {
        private readonly PatientDbContext _dbContext;

        public PatientOutboxRepository(PatientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(OutboxMessage message)
        {
            await _dbContext.OutboxMessages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }
    }
}
