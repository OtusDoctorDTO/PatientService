using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories
{
    public interface IMedicalRecordRepository
    {
        IEnumerable<MedicalRecord> GetByPatientId(int patientId);
        void Add(MedicalRecord medicalRecord);

    }
}
