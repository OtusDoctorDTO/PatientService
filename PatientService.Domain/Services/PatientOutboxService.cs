using HelpersDTO.Patient.DTO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PatientService.Domain.Entities;
using PatientService.Domain.Repositories;

namespace PatientService.Domain.Services
{
    public class PatientOutboxService : IPatientOutboxService
    {
        private readonly IPatientOutboxRepository _outboxRepository;

        public PatientOutboxService(IPatientOutboxRepository outboxRepository)
        {
            _outboxRepository = outboxRepository;
        }

        public async Task SaveOutboxMessage(OutboxMessage message)
        {
            await _outboxRepository.AddAsync(message);
        }
    }
}
