using PatientService.Domain.Entities;

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
