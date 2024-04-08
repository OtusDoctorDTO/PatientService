using PatientService.Domain.Entities;

namespace PatientService.Domain.Services
{
    //Интерфейс, определяющий операции, связанные с пациентами.
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatients();
        Task<Patient?> GetPatientById(int id);
        Task AddPatient(Patient patient);

    }
}
