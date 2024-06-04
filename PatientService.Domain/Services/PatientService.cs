using HelpersDTO.Patient.DTO;
using Microsoft.Extensions.Logging;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;
using System.Numerics;

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

        public async Task<PatientDTO?> GetById(Guid id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null) return null;
            return new PatientDTO()
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MiddleName = patient.MiddleName,
                Phone = patient.PhoneNumber,
                UserId = patient.UserId,
                IsNew = patient.IsNew
            };
        }

        public async Task<bool> AddAsync(PatientDTO patient)
        {
            try
            {
                var patientDB = new Patient()
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    MiddleName = patient.MiddleName,
                    PhoneNumber = patient.Phone,
                    UserId = patient.UserId,
                    IsNew = patient.IsNew
                };
                return await _repository.AddAsync(patientDB);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при добавлении пациента.");
            }
            return false;
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
                IsNew = p.IsNew
                //Status = 
            }).ToList();
        }
    }
}
