using HelpersDTO.CallCenter.DTO.Models;
using PatientService.Domain.Entities;

namespace PatientService.Domain.Services
{
    //Интерфейс, определяющий операции, связанные с пациентами.
    public interface IPatientService
    {
        Task<Patient?> GetPatientById(Guid id);
        Task<Guid> AddPatient(PatientDto patient);

    }
}
