using HelpersDTO.CallCenter.DTO.Models;
using HelpersDTO.CallCenter.DTO;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PatientService.API.Consumers;
using PatientService.Domain.Entities;
using PatientService.Domain.Services;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Patient>> GetPatientById(Guid id)
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
        public async Task<IActionResult> AddPatientAsync(Patient patient)
        {
            try
            {
                if (patient == null)
                {
                    return BadRequest("Пациент не заполнен.");
                }

                if (string.IsNullOrEmpty(patient.LastName) || string.IsNullOrEmpty(patient.FirstName))
                {
                    return BadRequest("Обязательные поля Имя и Фамилия не заполнены.");
                }

                await _patientService.AddAsync(patient);

                return Ok("Пациент успешно добавлен.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка Add");
                return BadRequest();
            }
        }

        [HttpPost("CreateTest")]
        public async Task<IActionResult> CreatePatient(PatientDto pacientDTO)
        {
            try
            {
                var responce = await _client.GetResponse<SavePatientDTOResponse>(new SavePatientDTORequest()
                {
                    Patient = pacientDTO,
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
