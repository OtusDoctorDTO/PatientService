using HelpersDTO.CallCenter.DTO.Models;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Services
{
    public class PatientService : IPatientService
    {

        public Task<Patient?> GetPatientById(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Guid> IPatientService.AddPatient(PatientDto patient)
        {
            throw new NotImplementedException();
        }
    }
}
