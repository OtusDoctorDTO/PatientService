
using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task UpdatePatient(int id);
        Task DeletePatient(int id);
    }
}
