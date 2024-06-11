using HelpersDTO.Patient;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PatientService.Domain.Services;
using HelpersDTO.Patient.DTO;
using HelpersDTO.CallCenter.DTO.Models;
using HelpersDTO.CallCenter.DTO;
using MassTransit.Transports;

namespace PatientService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;
        IRequestClient<SavePatientDTORequest> _client;
        private readonly IPublishEndpoint _publishEndpoint;

        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger, IRequestClient<SavePatientDTORequest> client, IPublishEndpoint publishEndpoint)
        {
            _patientService = patientService;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _client = client;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(PatientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientDTO>> GetPatientById(Guid userId)
        {
            try
            {
                _logger.LogInformation("Получение данных пациента с ID: {UserId}", userId);
                var patient = await _patientService.GetPatientByUserIdAsync(userId);
                if (patient == null)
                {
                    _logger.LogWarning("Пациент с ID: {UserId} не найден", userId);
                    return NotFound();
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при получении данных пациента с ID: {UserId}", userId);
                return BadRequest();
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPatientAsync([FromBody] PatientDTO patient)
        {
            try
            {
                if (patient == null)
                {
                    return BadRequest("Пациент не заполнен.");
                }

                await _publishEndpoint.Publish(patient);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка при добавлении пациента");
                return BadRequest();
            }
        }

        [HttpPost("GetByIds")]
        public async Task<IActionResult> GetByIdsAsync([FromBody] Guid[] usersId)
        {
            try
            {
                _logger.LogInformation("Получение данных пациентов по ID: {UserIds}", string.Join(", ", usersId));
                var result = await _patientService.GetByIds(usersId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при получении данных пациентов по ID");
                return BadRequest();
            }
        }

        [HttpPost("CreateTest")]
        public async Task<IActionResult> CreatePatient(PatientDto patientDTO)
        {
            try
            {
                var responce = await _client.GetResponse<SavePatientDTOResponse>(new SavePatientDTORequest()
                {
                    Patient = patientDTO,
                    Guid = Guid.NewGuid()
                });
                _logger.LogInformation("Получен ответ {responce}", responce.Message);
                return Ok(responce.Message.Id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка Add");
                return BadRequest();
            }
        }
    }
}
