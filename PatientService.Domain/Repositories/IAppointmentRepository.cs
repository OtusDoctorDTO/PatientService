using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAll();
        IEnumerable<Appointment> GetByPatientId(int patientId);
        void Add(Appointment appointment);
    }
}
