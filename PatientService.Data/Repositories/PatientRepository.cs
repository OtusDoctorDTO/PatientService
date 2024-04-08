using Microsoft.EntityFrameworkCore;
using PatientService.Data.Context;
using PatientService.Data.Entities;
using PatientService.Domain.Repositories;
using System.Numerics;

namespace PatientService.Data.Repositories
{
    public class PatientRepository 
    {
        private readonly PatientDbContext _dbContext;

        public PatientRepository(PatientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            return await _dbContext.Patients.ToListAsync();
        }

        public async Task<Patient?> GetPatientById(int id)
        {
            return await _dbContext.Patients.FindAsync(id);
        }

        public async Task AddPatient(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePatient(Patient patient)
        {
            _dbContext.Patients.Update(patient);
            await _dbContext.SaveChangesAsync();
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
