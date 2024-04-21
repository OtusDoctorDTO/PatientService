using HelpersDTO.CallCenter.DTO;
using MassTransit;
using PatientService.Domain.Entities;
using PatientService.Domain.Services;

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
                var newPatient = new Patient { Id = context.Message.Patient.UserId, FirstName = "Петр", LastName = "Петров" };
                await service.AddPatientAsync(newPatient);
                result.Success = true;
            }
            catch (System.Exception e)
            {
                logger.LogError(e, "При сохранении пациента произошла ошибка");
            }
            await context.RespondAsync(result);
        }
    }

    public class SavePatientDTOResponseConsumer : IConsumer<SavePatientDTOResponse>
    {
        public async Task Consume(ConsumeContext<SavePatientDTOResponse> context)
        {
            var response = context.Message;

            if (response.Success)
            {
                // Logic to handle successful message processing
                // For example, log success or perform additional actions
            }
            else
            {
                // Logic to handle failed message processing
                // For example, log failure or retry message processing
            }
        }
    }
}
