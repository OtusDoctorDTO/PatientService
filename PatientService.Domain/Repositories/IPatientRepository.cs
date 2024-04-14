using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient> GetPatientByIdAsync(Guid id);
        Task UpdatePatientAsync(Guid id);
        Task DeletePatientAsync(Guid id);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
    }
}
