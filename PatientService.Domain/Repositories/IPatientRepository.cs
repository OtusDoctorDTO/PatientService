
using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task UpdatePatient(Guid id);
        Task DeletePatient(Guid id);
    }
}
