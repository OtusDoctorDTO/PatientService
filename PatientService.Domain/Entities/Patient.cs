﻿namespace PatientService.Domain.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? UserId { get; set; }
        public bool IsNew { get; set; }
        public virtual List<Document>? Documents { get; set; } = new ();
    }
}
