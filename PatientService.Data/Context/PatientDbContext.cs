using HelpersDTO.CallCenter.DTO.Models;
using Microsoft.EntityFrameworkCore;
using PatientService.Domain.Entities;

namespace PatientService.Data.Context
{
    public class PatientDbContext : DbContext
    {
        public DbSet<PatientDto> Patients { get; set; }
        // Add DbSet properties for other entities (e.g., Appointments, MedicalRecords)

        public PatientDbContext(DbContextOptions<PatientDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity mappings and relationships
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatientDbContext).Assembly);
        }
    }
}
