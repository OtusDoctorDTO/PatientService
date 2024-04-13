using Microsoft.Extensions.Logging;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;
using HelpersDTO.CallCenter.DTO.Models;

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

        public Task<PatientDto> GetPatientById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PatientDto> AddPatient(PatientDto patient)
        {
            try
            {
                // Add logic to save the patient to the database using the repository
                var newPatient = await _repository.AddPatient(patient);

                _logger.LogInformation("Patient added successfully: {PatientId}", newPatient);
                return newPatient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the patient");
                throw; // Rethrow the exception to be handled by the caller
            }
        }
    }
}
