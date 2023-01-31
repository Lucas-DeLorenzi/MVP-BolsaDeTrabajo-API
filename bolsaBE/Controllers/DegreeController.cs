using bolsaBE.Models;
using bolsaBE.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bolsaBE.Controllers
{
    [Route("api/degrees")]
    [ApiController]
    [Authorize]
    public class DegreeController : ControllerBase
    {
        private readonly IBolsaDeTrabajoRepository _bolsaDeTrabajoRepository;
        private readonly IDegreesServices _degreesServices;

        public DegreeController(IBolsaDeTrabajoRepository bolsaDeTrabajoRepository, IDegreesServices degreesServices)
        {
            _bolsaDeTrabajoRepository = bolsaDeTrabajoRepository;
            _degreesServices = degreesServices;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DegreeDTO>> GetDegrees()
        {
            var degrees = _degreesServices.GetDegrees();
            return Ok(degrees);
        }

        [HttpGet("{id}", Name = "GetDegree")]
        public ActionResult<DegreeDTO> GetDegree(string id)
        {
            var degree = _degreesServices.GetDegreeById(id);
            if (degree is null)
                return NotFound();
            return Ok(degree);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult<DegreeDTO> AddDegree(DegreeToCreateDTO DegreeToCreate)
        {
            var degree = _degreesServices.AddDegree(DegreeToCreate);
            if (degree is null)
            {
                return BadRequest("No se pudo crear la carrera, revise los datos ingresados");
            }
            return CreatedAtRoute("GetDegree", new
            {
                id = degree.DegreeId
            }, degree
            );
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public ActionResult<DegreeDTO> UpdateDegree(DegreeToUpdDTO DegreeToUpd, string idDegree)
        {
            if(_degreesServices.GetDegreeById(idDegree) is null)
            {
                return NotFound();
            }
            if(_degreesServices.UpdateDegree(DegreeToUpd, idDegree))
            {
                return NoContent();
            };
            return BadRequest();
        }

        [HttpDelete("{idDegree}")]
        [Authorize(Roles = "Administrador")]
        public ActionResult<DegreeDTO> DeleteDegree(string idDegree)
        {
            if (_degreesServices.GetDegreeById(idDegree) is null)
            {
                return NotFound();
            }
            if (_degreesServices.DeleteDegree(idDegree))
            {
                return NoContent();
            };
            return BadRequest();
        }
    }
}
