﻿using PatientService.Domain.Entities;

namespace PatientService.Domain.Repositories
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
        void Add(Patient patient);

    }
}
