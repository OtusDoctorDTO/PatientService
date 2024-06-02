﻿namespace PatientService.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? UsersId { get; set; }

    }
}
