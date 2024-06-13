using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientService.Data.Context;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;

namespace PatientService.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _dbContext;
        private readonly ILogger<PatientRepository> _logger;

        public PatientRepository(PatientDbContext dbContext, ILogger<PatientRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task UpdateAsync(Guid id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _dbContext.Patients.Update(patient);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> AddAsync(Patient patient)
        {
            try
            {
                await _dbContext.Patients.AddAsync(patient);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении пациента с UserId {UserId}", patient.UserId);
                return false;
            }
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Patients.FindAsync(id);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _dbContext.Patients.ToListAsync();
        }

        public async Task<List<Patient>?> GetByIds(Guid[] usersId)
        {
            return await _dbContext.Patients
                .Include(p => p.Documents)
                .Where(p => p.UserId != null && usersId.Any(userId => userId == p.UserId))
                .ToListAsync();
        }
        public async Task<Patient?> GetByUserIdAsync(Guid? userId)
        {
            return await _dbContext.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
