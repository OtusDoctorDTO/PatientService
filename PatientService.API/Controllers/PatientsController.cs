using HelpersDTO.Patient;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PatientService.Domain.Services;
using HelpersDTO.Patient.DTO;

namespace PatientService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(PatientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientDTO>> GetPatientById(Guid userId)
        {
            try
            {
                _logger.LogInformation("Получение данных пациента с ID: {UserId}", userId);
                var patient = await _patientService.GetByUserIdAsync(userId);
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
                    _logger.LogWarning("Попытка добавить пустого пациента");
                    return BadRequest("Пациент не заполнен.");
                }
                var result = await _patientService.AddAsync(patient);
                _logger.LogInformation("Пациент добавлен: {Result}", result);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при добавлении пациента");
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
    }
}
