using bolsaBE.Models.Users;
using bolsaBE.Models.Users.Student;
using bolsaBE.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bolsaBE.Controllers
{
    [Route("api/student")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        public StudentController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            var students = _studentServices.GetStudents();
            return Ok(students);
        }

        [HttpGet("GetCurrentPersonalData")]
        [Authorize(Roles = "Alumno")]
        public ActionResult<StudentDTO> GetCurrentPersonalData()
        {

            var student = _studentServices.GetCurrentPersonalData();
            if (student is null)
                return NotFound();
            return Ok(student);
        }

        [HttpGet("GetCurrentOtherData")]
        [Authorize(Roles = "Alumno")]
        public ActionResult<OtherDataDTO> GetCurrentOtherData()
        {
            var student = _studentServices.GetCurrentOtherData();
            if (student is null)
                return NoContent();
            return Ok(student);
        }

        [HttpGet("GetCurrentUniversityData")]
        [Authorize(Roles ="Alumno")]
        public ActionResult<UniversityDataDTO> GetCurrentUniversityData()
        {
            var studentUniversityData = _studentServices.GetCurrentUniversityData();
            if (studentUniversityData is null)
                return NoContent();
            return Ok(studentUniversityData);
        }

        [HttpGet("CheckUniqueFields")]
        [Authorize (Roles ="Alumno")]
        public ActionResult<string> CheckUniqueFields(string field, string value)
        {
            try
            {
                if (_studentServices.CheckUniqueFields(field, value))
                    return Ok($"{field} is not valid");
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult StudentSignUp(StudentToCreateDTO student)
        {
            try
            {
                var newStudent = _studentServices.StudentSignUp(student);
                if (newStudent is null)
                    return BadRequest();
                return Created("/api/student", newStudent);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("/api/student/toValidate")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<IEnumerable<StudentToValidateDTO>> GetStudentsToValidate()
        {
            var students = _studentServices.GetStudentsToValidate();
            return Ok(students);
        }

        [HttpPut("toValidate/{id}", Name = "PutUpdatedAtForStudent")]
        [Authorize(Roles = "Administrador")]
        public ActionResult PutUpdatedAtForStudent(string id)
        {
            var result = _studentServices.PutUpdatedAtForStudent(id);
            switch (result)
            {
                case "Ok":
                    return Ok("El usuario ha sido validado");
                case "Not Found":
                    return NotFound();
                default:
                    return BadRequest(result);
            }
        }

        [HttpPut("updateOtherData")]
        [Authorize(Roles = "Alumno")]
        public ActionResult<bool> StudentUpdateOtherData([FromForm] OtherDataToUpdDTO otherData)
        {
            try
            {
                _studentServices.StudentUpdateOtherData(otherData);
                return Ok(true);
            }
            catch
            {
                return BadRequest(false);
            }
        }

        [HttpPut("updatePersonalData")]
        [Authorize(Roles ="Alumno")]
        public ActionResult UpdateStudentPersonalData(StudentPersonalDataDTO studentPersonalData)
        {
            try
            {
                if (_studentServices.AnyRequiredFieldIsNull(studentPersonalData))
                    return BadRequest("Los campos obligatorios no pueden ser nulos");
                if (_studentServices.UpdateStudentPersonalData(studentPersonalData))
                    return Ok("Usuario actualizado");
                return BadRequest("Algo salio mal");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("updateUniversityData")]
        [Authorize(Roles = "Alumno")]
        public ActionResult UpdateUniversityData(UniversityDataToUpdDTO universityData)
        {
            try
            {
                if(_studentServices.StudentUpdateUniversityData(universityData)) 
                    return Ok("Datos actualizados correctamente");
                return BadRequest("Ocurrió un error");
            }
            catch
            {
                return BadRequest("Ocurrió un error");
            }
        }

        [HttpDelete("{studentId}")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<bool> RemoveStudent(Guid studentId)
        {
            try
            {
                return Ok(_studentServices.RemoveStudent(studentId));

            } catch
            {
                return BadRequest();
            }
        }
    }
}
