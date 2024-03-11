using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Services
{
    //Интерфейс, определяющий операции, связанные с встречами.
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAllAppointments();
        IEnumerable<Appointment> GetAppointmentsByPatientId(int patientId);
        void ScheduleAppointment(Appointment appointment);
    }
}
