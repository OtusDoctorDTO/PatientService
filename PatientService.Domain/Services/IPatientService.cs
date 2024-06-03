using HelpersDTO.Patient.DTO;
using PatientService.Domain.Entities;

namespace PatientService.Domain.Services
{
    //Интерфейс, определяющий операции, связанные с пациентами.
    public interface IPatientService
    {
        Task<Patient> GetById(Guid id);
        Task<Patient> AddAsync(Patient patient);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task UpdateAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<List<PatientDTO>?> GetByIds(Guid[] usersId);
    }
}
