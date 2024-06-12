using HelpersDTO.CallCenter.DTO;
using HelpersDTO.Patient;
using HelpersDTO.Patient.DTO;
using MassTransit;
using PatientService.Domain.Services;
using static MassTransit.ValidationResultExtensions;

namespace PatientService.API.Consumers
{
    public class PatientConsumer : IConsumer<CreatePatientRequest>, IConsumer<SavePatientDTORequest>
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

        public async Task Consume(ConsumeContext<CreatePatientRequest> context)
        {
            _logger.LogInformation("Получен запрос CreatePatientRequest {message}", context.Message);
            var response = new CreatePatientResponse()
            {
                Guid = context.Message.Guid,
                Success = true,
                ConnectionId = context.Message.ConnectionId
            };
            try
            {
                var patientDto = context.Message.Patient;
                await _service.AddAsync(patientDto);
                _logger.LogInformation("Пациент успешно добавлен с UserId {UserId}", patientDto.UserId);
            }
            catch (Exception ex)
            {
                response.Success = false;
                _logger.LogError(ex, "При добавлении пациента CreatePatientRequest произошла ошибка");
            }
            await context.RespondAsync(response);
        }
    }
}
