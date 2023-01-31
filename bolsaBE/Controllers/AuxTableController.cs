using bolsaBE.Data;
using bolsaBE.Entities;
using bolsaBE.Models;
using bolsaBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace bolsaBE.Controllers
{
    [ApiController]
    [Route("api/aux")]
    public class AuxTableController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private IFullService _fullService;

        public AuxTableController(
            IUnitOfWork unitOfWork,
            IFullService fullService
            )
        {
            _unitOfWork = unitOfWork;
            _fullService = fullService;
        }
        [HttpGet("CivilStatus")]
        public async Task<IEnumerable<AuxTableDTO>> AllCivilStatus()
        {
            return await _fullService.GetAllCivilStatusTypes();
        }
        [HttpGet("GenderTypes")]
        public async Task<IEnumerable<AuxTableDTO>> AllGenderTypes()
        {
            return await _fullService.GetAllGenderTypes();
        }
        [HttpGet("KnowledgeValues")]
        public async Task<IEnumerable<AuxTableDTO>> AllKnowledgeValues()
        {
            return await _fullService.GetAllKnowledgeValues();
        }
        [HttpGet("WorkdayTypes")]
        public async Task<IEnumerable<AuxTableDTO>> AllWorkdayTypes()
        {
            return await _fullService.GetAllWorkdayTypes();
        }

        //DocumentTypes
        [HttpGet("DocumentTypes")]
        public async Task<IEnumerable<AuxTableDTO>> AllDocumentTypes()
        {
            return await _fullService.GetAllDocumentTypes();
        }

        [HttpPost("DocumentTypes")]
        public async Task<bool> InsertDocumentType([FromBody] string name)
        {
            return await _fullService.InsertDocumentType(name);
        }
        [HttpPut("DocumentTypes")]
        public async Task<bool> UpdateDocumentType(AuxTableDTO element)
        {
            return await _fullService.UpdateDocumentType(element);
        }
        [HttpDelete("DocumentTypes/{id}")]
        public async Task<bool> UpdateDocumentTypes(Guid id)
        {
            return await _fullService.DeleteDocumentType(id);
        }

        // KnowledgeTypes
        [HttpGet("KnowledgesTypes")]
        public async Task<IEnumerable<AuxTableDTO>> AllKnowledgesTypes()
        {
            return await _fullService.GetAllKnowledgesTypes();
        }

        [HttpPost("KnowledgesTypes")]
        public async Task<bool> InsertKnowledgesType([FromBody] string name)
        {
            return await _fullService.InsertKnowledgeType(name);
        }
        [HttpPut("KnowledgesTypes")]
        public async Task<bool> UpdateKnowledgesType(AuxTableDTO element)
        {
            return await _fullService.UpdateKnowledgeType(element);
        }
        [HttpDelete("KnowledgesTypes/{id}")]
        public async Task<bool> UpdateKnowledgesType(Guid id)
        {
            return await _fullService.DeleteKnowledgeType(id);
        }

        //RelationTypes
        [HttpGet("RelationTypes")]
        public async Task<IEnumerable<AuxTableDTO>> AllRelationTypes()
        {
            return await _fullService.GetAllRelationTypes();
        }
        [HttpPost("RelationTypes")]
        public async Task<bool> InsertRelationType([FromBody] string name)
        {
            return await _fullService.InsertRelationType(name);
        }
        [HttpPut("RelationTypes")]
        public async Task<bool> UpdateRelationType(AuxTableDTO element)
        {
            return await _fullService.UpdateRelationType(element);
        }
        [HttpDelete("RelationTypes/{id}")]
        public async Task<bool> DeleteRelationType(Guid id)
        {
            return await _fullService.DeleteRelationType(id);
        }

    }
}
