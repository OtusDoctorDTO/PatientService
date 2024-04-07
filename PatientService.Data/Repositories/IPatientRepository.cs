using PatientService.Data.Entities;

namespace PatientService.Data.Repositories
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
        void Add(Patient patient);

    }
}
