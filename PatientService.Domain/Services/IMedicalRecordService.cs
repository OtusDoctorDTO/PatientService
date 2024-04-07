using PatientService.Domain.Entities;

namespace PatientService.Domain.Services
{
    public interface IMedicalRecordService
    {
        IEnumerable<MedicalRecord> GetMedicalRecordsByPatientId(int patientId);
        void AddMedicalRecord(MedicalRecord medicalRecord);
    }
}
