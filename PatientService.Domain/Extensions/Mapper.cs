using HelpersDTO.Patient.DTO;
using PatientService.Domain.Entities;

namespace PatientService.API.Extensions
{
    public static class Mapper
    {
        public static Patient? ToPatientDB(this PatientDTO? patient)
        {
            if (patient == null) return null;
            return new Patient()
            {
                UserId = patient.UserId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.Phone,
                IsNew = patient.IsNew
            };
        }
        public static PatientDTO? ToPatientDto(this Patient? patient)
        {
            if (patient == null) return null;
            return new PatientDTO()
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MiddleName = patient.MiddleName,
                Phone = patient.PhoneNumber,
                UserId = patient.UserId,
                IsNew = patient.IsNew
            };
        }
    }
}
