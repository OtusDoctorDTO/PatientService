using HelpersDTO.Patient.DTO;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PatientService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Services
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IPatientOutboxRepository _outboxRepository;
        private readonly ILogger<OutboxProcessor> _logger;
        private readonly IBus _bus;

        public OutboxProcessor(IPatientOutboxRepository outboxRepository, ILogger<OutboxProcessor> logger, IBus bus)
        {
            _outboxRepository = outboxRepository;
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var messages = await _outboxRepository.GetUnprocessedMessagesAsync();

                foreach (var message in messages)
                {
                    try
                    {
                        var patient = JsonConvert.DeserializeObject<PatientDTO>(message.Payload);
                        await _bus.Publish(patient);

                        message.ProcessedOn = DateTime.UtcNow;
                        await _outboxRepository.UpdateAsync(message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Ошибка при обработке сообщения из Outbox");
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
