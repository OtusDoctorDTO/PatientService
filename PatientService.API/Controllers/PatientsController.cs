using HelpersDTO.CallCenter.DTO.Models;
using HelpersDTO.CallCenter.DTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PatientService.Domain.Services;
using HelpersDTO.Patient.DTO;

namespace PatientService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;
        IRequestClient<SavePatientDTORequest> _client;

        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger, IRequestClient<SavePatientDTORequest> client)
        {
            _patientService = patientService;
            _logger = logger;
            _client = client;
        }

        [HttpGet("{userid}")]
        [ProducesResponseType(typeof(PatientDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientDTO>> GetPatientById(Guid id)
        {
            try
            {
                var patient = await _patientService.GetById(id);
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

        [HttpPost("Add")]
        public async Task<IActionResult> AddPatientAsync(PatientDTO patient)
        {
            try
            {
                if (patient == null)
                {
                    return BadRequest("Пациент не заполнен.");
                }
                var result = await _patientService.AddAsync(patient);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка Add");
                return BadRequest();
            }
        }

        [HttpPost("GetByIds")]
        public async Task<IActionResult> GetByIdsAsync(Guid[] usersId)
        {
            try
            {
                var result = await _patientService.GetByIds(usersId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка GetByIds");
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
