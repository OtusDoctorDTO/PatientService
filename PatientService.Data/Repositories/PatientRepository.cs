

using HelpersDTO.CallCenter.DTO.Models;
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

        public async Task UpdatePatient(Guid id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _dbContext.Patients.Update(patient);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeletePatient(Guid id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PatientDto> AddPatient(PatientDto patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
            return patient;
        }

        public async Task<PatientDto> GetPatientByIdAsync(Guid id)
        {
            return await _dbContext.Patients.FindAsync(id);
        }
    }
}
