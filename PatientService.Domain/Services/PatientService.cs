using HelpersDTO.Patient.DTO;
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

        public async Task<Patient?> GetById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Patient?> AddAsync(Patient patient)
        {
            try
            {
                var newPatient = await _repository.AddAsync(patient);

                _logger.LogInformation("Пациент успешно добавлен: {PatientId}", newPatient);
                return newPatient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при добавлении пациента.");
                throw;
            }
        }

        public async Task<IEnumerable<Patient>?> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateAsync(Guid id)
        {
            await _repository.UpdateAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<PatientDTO>?> GetByIds(Guid[] usersId)
        {
            var patients = await _repository.GetByIds(usersId);
            if (patients?.Any() ?? true) return null;
            return patients!.Select(p => new PatientDTO()
            {
                Id = p.Id,
                UserId = p.UserId,
                LastName = p.LastName ?? "",
                FirstName = p.FirstName ?? "",
                MiddleName = p.MiddleName ?? "",
                IsNew = p.Documents?.Any() ?? false
                //Status = 
            }).ToList();
        }
    }
}
