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
                var patient = await _patientService.GetById(userId);
                if (patient == null)
                {
                    return NotFound();
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка GetPatientById");
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
                var result = await _patientService.AddAsync(patient);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Не удалось добавить пациента.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка Add");
                return BadRequest();
            }
        }

        [HttpPost("GetByIds")]
        public async Task<IActionResult> GetByIdsAsync([FromBody] Guid[] usersId)
        {
            try
            {
                var result = await _patientService.GetByIds(usersId);
                if (result == null || !result.Any())
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка GetByIds");
                return BadRequest();
            }
        }
    }
}
