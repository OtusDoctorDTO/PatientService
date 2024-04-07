using PatientService.Data.Entities;

namespace PatientService.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _dbContext;

        public PatientRepository(PatientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Patient> GetAll()
        {
            return _dbContext.Patients.ToList();
        }

        public Patient GetById(int id)
        {
            return _dbContext.Patients.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            _dbContext.SaveChanges();
        }
    }
}
