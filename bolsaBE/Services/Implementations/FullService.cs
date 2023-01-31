using AutoMapper;
using bolsaBE.Data;
using bolsaBE.Entities;
using bolsaBE.Entities.Auxiliary.Types.Implementation;
using bolsaBE.Models;

namespace bolsaBE.Services
{
    public class FullService : IFullService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FullService (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task <List<AuxTableDTO>> GetAllCivilStatusTypes()
        {
            var civilStatusTypes = await _unitOfWork.CivilStatusTypes.GetAllAsync();
            return _mapper.Map<List<AuxTableDTO>>(civilStatusTypes);
        }
        public async Task<List<AuxTableDTO>> GetAllGenderTypes()
        {
            var genderTypes = await _unitOfWork.GenderTypes.GetAllAsync();
            return _mapper.Map<List<AuxTableDTO>>(genderTypes);
        }

        public async Task<List<AuxTableDTO>> GetAllKnowledgeValues()
        {
            var knowledgeValues = await _unitOfWork.KnowledgeValues.GetAllAsync();
            return _mapper.Map<List<AuxTableDTO>>(knowledgeValues);
        }

        public async Task<List<AuxTableDTO>> GetAllWorkdayTypes()
        {
            var workdayTypes = await _unitOfWork.WorkdayTypes.GetAllAsync();
            return _mapper.Map<List<AuxTableDTO>>(workdayTypes);
        }

        // DocumentTypes
        public async Task<List<AuxTableDTO>> GetAllDocumentTypes()
        {
            var documentTypes = await _unitOfWork.DocumentTypes.GetAllAsync();
            return _mapper.Map<List<AuxTableDTO>>(documentTypes);
        }

        public Task<bool> InsertDocumentType(string elementToCreate)
        {
            var element = new DocumentType() { Name = elementToCreate };
            _unitOfWork.DocumentTypes.Add(element);
            return _unitOfWork.KnowledgeTypes.SaveAsync();
        }
        public Task<bool> UpdateDocumentType(AuxTableDTO elementToUpdate)
        {
            var element = _unitOfWork.DocumentTypes.GetByIdAsync(elementToUpdate.Id).Result;
            if (element is null) return Task.FromResult(false);
            element.Name = elementToUpdate.Name;
            _unitOfWork.DocumentTypes.Update(element);
            return _unitOfWork.DocumentTypes.SaveAsync();
        }
        public Task<bool> DeleteDocumentType(Guid elementIdToDelete)
        {
            var element = _unitOfWork.DocumentTypes.GetByIdAsync(elementIdToDelete).Result;
            if (element is null) return Task.FromResult(false);
            _unitOfWork.DocumentTypes.Remove(element);
            return _unitOfWork.DocumentTypes.SaveAsync();
        }

        // KnowledgeTypes
        public async Task<List<AuxTableDTO>> GetAllKnowledgesTypes()
        {
            var knowledgesTypes = await _unitOfWork.KnowledgeTypes.GetAllAsync();
            return _mapper.Map<List<AuxTableDTO>>(knowledgesTypes);
        }

        public Task<bool> InsertKnowledgeType(string knowledgeToCreate)
        {
            var knowledge = new KnowledgeType() { Name = knowledgeToCreate };
            _unitOfWork.KnowledgeTypes.Add(knowledge);
            return _unitOfWork.KnowledgeTypes.SaveAsync();
        }
        public Task<bool> UpdateKnowledgeType(AuxTableDTO knowledgeToUpdate)
        {
            var knowledge = _unitOfWork.KnowledgeTypes.GetByIdAsync(knowledgeToUpdate.Id).Result;
            if (knowledge is null) return Task.FromResult(false);
            knowledge.Name = knowledgeToUpdate.Name;
            _unitOfWork.KnowledgeTypes.Update(knowledge);
            return _unitOfWork.KnowledgeTypes.SaveAsync();
        }
        public Task<bool> DeleteKnowledgeType(Guid knowledgeIdToDelete)
        {
            var knowledge = _unitOfWork.KnowledgeTypes.GetByIdAsync(knowledgeIdToDelete).Result;
            if (knowledge is null) return Task.FromResult(false);
            _unitOfWork.KnowledgeTypes.Remove(knowledge);
            return _unitOfWork.KnowledgeTypes.SaveAsync();
        }

        // RelationTypes
        public async Task<List<AuxTableDTO>> GetAllRelationTypes()
        {
            var relationTypes = await _unitOfWork.RelationTypes.GetAllAsync();
            return _mapper.Map<List<AuxTableDTO>>(relationTypes);
        }

        public Task<bool> InsertRelationType(string relationToCreate)
        {
            var relation = new RelationType() { Name = relationToCreate };
            _unitOfWork.RelationTypes.Add(relation);
            return _unitOfWork.RelationTypes.SaveAsync();
        }
        public Task<bool> UpdateRelationType(AuxTableDTO relationToUpdate)
        {
            var relation = _unitOfWork.RelationTypes.GetByIdAsync(relationToUpdate.Id).Result;
            if (relation is null) return Task.FromResult(false);
            relation.Name = relationToUpdate.Name;
            _unitOfWork.RelationTypes.Update(relation);
            return _unitOfWork.RelationTypes.SaveAsync();
        }
        public Task<bool> DeleteRelationType(Guid relationIdToDelete)
        {
            var relation = _unitOfWork.RelationTypes.GetByIdAsync(relationIdToDelete).Result;
            if (relation is null) return Task.FromResult(false);
            _unitOfWork.RelationTypes.Remove(relation);
            return _unitOfWork.RelationTypes.SaveAsync();
        }

    }
}
