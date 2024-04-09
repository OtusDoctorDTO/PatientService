using Microsoft.EntityFrameworkCore;
using PatientService.Data.Context;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;

namespace PatientService.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _dbContext;

        public PatientRepository(PatientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdatePatient(int id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _dbContext.Patients.Update(patient);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeletePatient(int id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
