using HelpersDTO.CallCenter.DTO;
using HelpersDTO.Patient.DTO;
using MassTransit;
using PatientService.Domain.Services;

namespace PatientService.API.Consumers
{
    public class PatientConsumer : IConsumer<SavePatientDTORequest>
    {
        private readonly ILogger<PatientConsumer> _logger;
        private readonly IPatientService _patientService;

        public PatientConsumer(ILogger<PatientConsumer> logger, IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
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
                var patientDTO = context.Message;
                var result = await _patientService.AddAsync(patientDTO);
                if (!result)
                {
                    _logger.LogError("Ошибка при добавлении пациента: {PatientId}", patientDTO.UserId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обработке сообщения пациента");
            }
        }
    }
}
