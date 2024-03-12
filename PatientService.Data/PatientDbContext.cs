using Microsoft.EntityFrameworkCore;
using PatientService.Data.Entities;

namespace PatientService.Data
{
    public class PatientDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
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
