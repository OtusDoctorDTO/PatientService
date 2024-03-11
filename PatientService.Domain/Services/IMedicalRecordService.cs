using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Services
{
    public interface IMedicalRecordService
    {
        IEnumerable<MedicalRecord> GetMedicalRecordsByPatientId(int patientId);
        void AddMedicalRecord(MedicalRecord medicalRecord);
    }
}
