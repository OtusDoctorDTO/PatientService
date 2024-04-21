using Microsoft.Extensions.Logging;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;

namespace PatientService.Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;
        private readonly IPatientRepository _repository;

        public PatientService(ILogger<PatientService> logger, IPatientRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Patient> GetPatientById(Guid id)
        {
            return await _repository.GetPatientByIdAsync(id);
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            try
            {
                var newPatient = await _repository.AddPatientAsync(patient);

                _logger.LogInformation("Patient added successfully: {PatientId}", newPatient);
                return newPatient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the patient");
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _repository.GetAllPatientsAsync();
        }

        public async Task UpdateAsync(Guid id)
        {
            await _repository.UpdatePatientAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeletePatientAsync(id);
        }
    }
}
