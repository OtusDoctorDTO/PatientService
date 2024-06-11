using HelpersDTO.CallCenter.DTO;
using HelpersDTO.Patient.DTO;
using MassTransit;
using PatientService.Domain.Services;

namespace PatientService.API.Consumers
{
    public class PatientConsumer : IConsumer<PatientDTO>, IConsumer<SavePatientDTORequest>
    {
        private readonly ILogger<PatientConsumer> _logger;
        private readonly IPatientService _service;

        public PatientConsumer(ILogger<PatientConsumer> logger, IPatientService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task Consume(ConsumeContext<SavePatientDTORequest> context)
        {
            _logger.LogInformation("Получен запрос SavePatientDTORequest {message}", context.Message);
            var result = new SavePatientDTOResponse()
            {
                Guid = context.Message.Guid,
                Success = true,
                ConnectionId = context.Message.ConnectionId
            };
            try
            {
                result.Success = true;
            }
            catch (System.Exception e)
            {
                _logger.LogError(e, "При сохранении пациента произошла ошибка");
            }
            await context.RespondAsync(result);
        }

        public async Task Consume(ConsumeContext<PatientDTO> context)
        {
            try
            {
                var patientDto = context.Message;
                await _service.AddAsync(patientDto);
                _logger.LogInformation("Пациент успешно добавлен с UserId {UserId}", patientDto.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке сообщения о добавлении пациента с UserId {UserId}", context.Message.UserId);
            }
        }
    }
}
