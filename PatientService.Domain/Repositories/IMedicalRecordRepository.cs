using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Repositories
{
    public interface IMedicalRecordRepository
    {
        IEnumerable<MedicalRecord> GetByPatientId(int patientId);
        void Add(MedicalRecord medicalRecord);

    }
}
