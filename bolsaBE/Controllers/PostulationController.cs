using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.Models.Postulations;
using bolsaBE.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bolsaBE.Controllers
{
    [Route("api/postulation")]
    [ApiController]
    public class PostulationController : ControllerBase
    {
        private readonly IPostulationServices _postulationServices;

        public PostulationController(IPostulationServices postulationServices)
        {
            _postulationServices = postulationServices;
        }

        [HttpPost]
        [Authorize(Roles = "Alumno")]
        public ActionResult<bool> Postulate(Guid searchId)
        {
            return Ok(_postulationServices.Postulate(searchId));
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<PostulationDTO>> GetPostulations()
        {
            return Ok(_postulationServices.GetPostulations());
        }

        [HttpDelete]
        [Authorize(Roles = "Alumno, Administrador")]
        public ActionResult<bool> UnPostulate(Guid postulationId)
        {
            return Ok(_postulationServices.UnPostulate(postulationId));
        }

    }
}
