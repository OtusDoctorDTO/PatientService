using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatients();
        Task<Patient?> GetPatientById(int id);
        Task AddPatient(Patient patient);
        Task UpdatePatient(Patient patient);

        Task DeletePatient(int id);
    }
}
