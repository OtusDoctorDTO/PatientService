using HelpersDTO.CallCenter.DTO.Models;
using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<PatientDto> AddPatient(PatientDto patient);
        Task<PatientDto> GetPatientByIdAsync(Guid id);
        Task UpdatePatient(Guid id);
        Task DeletePatient(Guid id);
    }
}
