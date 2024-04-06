using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAll();
        IEnumerable<Appointment> GetByPatientId(int patientId);
        void Add(Appointment appointment);
    }
}
