using Microsoft.AspNetCore.Mvc;
using PatientService.Domain.Entities;
using PatientService.Domain.Services;

namespace PatientService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAllPatients()
        {
            var patients = _patientService.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatientById(int id)
        {
            var patient = _patientService.GetPatientById(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

    }
}
