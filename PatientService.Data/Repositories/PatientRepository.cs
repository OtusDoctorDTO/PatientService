﻿using PatientService.Data.Context;
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

        public async Task UpdatePatientAsync(Guid id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _dbContext.Patients.Update(patient);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeletePatientAsync(Guid id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> GetPatientByIdAsync(Guid id)
        {
            return await _dbContext.Patients.FindAsync(id);
        }

        public Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
