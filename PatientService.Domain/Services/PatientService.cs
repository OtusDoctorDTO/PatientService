using HelpersDTO.Patient.DTO;
using PatientService.API.Extensions;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;

namespace PatientService.Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<PatientDTO?> GetById(Guid id)
        {
            var patient = await _repository.GetByIdAsync(id);
            return patient.ToPatientDto();
        }

        public async Task AddAsync(PatientDTO? patientDto)
        {
            var patientDB = patientDto.ToPatientDB();
            await _repository.AddAsync(patientDB);
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

        public async Task<PatientDTO?> GetPatientByUserIdAsync(Guid? userId)
        {
            var patient = await _repository.GetByUserIdAsync(userId);
            return patient.ToPatientDto();  
        }
    }
}
