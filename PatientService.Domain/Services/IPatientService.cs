using HelpersDTO.CallCenter.DTO.Models;
using PatientService.Domain.Entities;

namespace PatientService.Domain.Services
{
    //Интерфейс, определяющий операции, связанные с пациентами.
    public interface IPatientService
    {
        Task<Patient> GetPatientById(Guid id);
        Task<Patient> AddPatientAsync(Patient patient);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task UpdateAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
