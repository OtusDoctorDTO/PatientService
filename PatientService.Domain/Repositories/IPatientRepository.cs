using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<bool> AddAsync(Patient patient);
        Task<Patient?> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<List<Patient>?> GetByIds(Guid[] usersId);
        Task<Patient?> GetByUserIdAsync(Guid? userId);
    }
}
