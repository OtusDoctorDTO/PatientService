using HelpersDTO.Patient.DTO;
using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Services
{
    public interface IPatientOutboxService
    {
        Task SaveOutboxMessage(OutboxMessage message);
    }
}
