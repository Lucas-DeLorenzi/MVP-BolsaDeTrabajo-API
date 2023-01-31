using bolsaBE.Models;

namespace bolsaBE.Services
{
    public interface IFullService
    {
        Task<List<AuxTableDTO>> GetAllCivilStatusTypes();
        Task<List<AuxTableDTO>> GetAllKnowledgeValues();
        Task<List<AuxTableDTO>> GetAllGenderTypes();
        Task<List<AuxTableDTO>> GetAllWorkdayTypes();

        Task<List<AuxTableDTO>> GetAllDocumentTypes();
        Task<bool> InsertDocumentType(string elementToCreate);
        Task<bool> UpdateDocumentType(AuxTableDTO elementToUpdate);
        Task<bool> DeleteDocumentType(Guid elementIdToDelete);
        
        Task<List<AuxTableDTO>> GetAllKnowledgesTypes();
        Task<bool> InsertKnowledgeType(string knowledgeToCreate);
        Task<bool> UpdateKnowledgeType(AuxTableDTO knowledgeToUpdate);
        Task<bool> DeleteKnowledgeType(Guid knowledgeIdToDelete);

        Task<List<AuxTableDTO>> GetAllRelationTypes();
        Task<bool> InsertRelationType(string relationToCreate);
        Task<bool> UpdateRelationType(AuxTableDTO relationToUpdate);
        Task<bool> DeleteRelationType(Guid relationIdToDelete);

    }
}
