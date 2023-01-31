using bolsaBE.Entities.Auxiliary.Types.Implementation;
using bolsaBE.Models;
using static bolsaBE.Data.GenericRepository.IGenericRepository;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IKnowledgeTypeRepository : IGenericRepository<KnowledgeType>
    {
        KnowledgeType? GetKnowledgeById(string idKnowledgeType);
        
        //IEnumerable<KnowledgeDTO>? GetKnowledgeType();
        //bool AddKnowledgeType(Guid knowledgeTypeId);
        //bool RemoveKnowledgeType(Guid addKnowledgeTypeId);
    }
}
