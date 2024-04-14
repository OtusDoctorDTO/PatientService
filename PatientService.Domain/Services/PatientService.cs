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

        public async Task<Patient> GetPatientById(Guid id)
        {
            return await _repository.GetPatientByIdAsync(id);
        }

        public async Task<Patient> AddPatient(Patient patient)
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
    }
}
