using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientService.Domain.Entities;
using PatientService.Domain.Services;

namespace PatientService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Patient>> GetPatientById(Guid id)
        {
            try
            {
                var patient = await _patientService.GetPatientById(id);
                if (patient == null)
                {
                    return NotFound();
                }
                return patient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка GetPatientById");
                return BadRequest();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddPatientAsync(Patient patient)
        {
            if (patient == null)
            {
                return BadRequest("Patient data is missing.");
            }

            // Вызываем метод сервиса для добавления пациента
            await _patientService.AddPatient(patient);

            return Ok("Patient added successfully.");
        }

    }
}
