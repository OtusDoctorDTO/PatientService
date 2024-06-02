using Microsoft.EntityFrameworkCore;
using PatientService.Domain.Entities;

namespace PatientService.Data.Context
{
    public class PatientDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Document> Documents { get; set; }

        public PatientDbContext(DbContextOptions<PatientDbContext> options)
    : base(options)
        {
            Database.Migrate();
        }
    }
}
