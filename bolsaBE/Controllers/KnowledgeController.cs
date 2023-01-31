using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bolsaBE.Controllers
{ 
    [Route("api/knowledge")]
    [ApiController]
    public class KnowledgeController : ControllerBase
    {
        private readonly IKnowledgeRepository _knowledgeServices;

        public KnowledgeController(IKnowledgeRepository knowledgeServices)
        {
            _knowledgeServices = knowledgeServices;
        }
        [HttpGet]
        public ActionResult<List<KnowledgeDTO>> GetKnowledge()
        {
            return Ok(_knowledgeServices.GetKnowledge());
        }

        [HttpPost]
        [Authorize(Roles = "Alumno")]
        public ActionResult<bool> AddKnowledge(KnowledgeToInsertDTO knowledge)
        {
            return Ok(_knowledgeServices.AddKnowledge(knowledge));
        }

        [HttpDelete]
        [Authorize(Roles = "Alumno")]
        public ActionResult<bool> KnowledgeToRemove(Guid knowledgeId)
        {
            return Ok(_knowledgeServices.RemoveKnowledge(knowledgeId));
        }
    }
}
