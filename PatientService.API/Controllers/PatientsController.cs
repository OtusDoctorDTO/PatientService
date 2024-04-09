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
        public ActionResult<Patient> GetPatientById(int id)
        {
            try
            {
                var patient = _patientService.GetPatientById(id);
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

    }
}
