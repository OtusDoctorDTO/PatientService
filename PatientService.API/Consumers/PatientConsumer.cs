using HelpersDTO.CallCenter.DTO;
using MassTransit;
using Microsoft.Extensions.Logging;
using PatientService.Domain.Services;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace PatientService.API.Consumers
{
    public class PatientConsumer : IConsumer<SavePatientDTORequest>
    {
        private readonly ILogger<PatientConsumer> logger;
        private readonly IPatientService service;

        public PatientConsumer(ILogger<PatientConsumer> logger, IPatientService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public async Task Consume(ConsumeContext<SavePatientDTORequest> context)
        {
            logger.LogInformation("Получен запрос SavePatientDTORequest {message}", context.Message);
            var result = new SavePatientDTOResponse()
            {
                Guid = context.Message.Guid,
                Success = true,
                ConnectionId = context.Message.ConnectionId
            };
            try
            {
                result.Guid = await service.AddPatient(context.Message.Patient);
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "При сохранении пациента произошла ошибка");
            }
            await context.RespondAsync(result);
        }
    }
}
