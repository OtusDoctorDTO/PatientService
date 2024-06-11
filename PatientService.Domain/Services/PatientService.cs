using HelpersDTO.Patient.DTO;
using MassTransit;
using Microsoft.Extensions.Logging;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;

namespace PatientService.Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;
        private readonly IPatientRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public PatientService(ILogger<PatientService> logger, IPatientRepository repository, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _repository = repository;
            _publishEndpoint = publishEndpoint;
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

        public async Task AddAsync(PatientDTO patientDto)
        {
            var patient = new Patient
            {
                UserId = patientDto.UserId,
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                PhoneNumber = patientDto.Phone,
                IsNew = patientDto.IsNew
            };

            await _repository.AddAsync(patient);
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

        public async Task<PatientDTO> GetPatientByUserIdAsync(Guid userId)
        {
            var patient = await _repository.GetByUserIdAsync(userId);
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
    }
}
