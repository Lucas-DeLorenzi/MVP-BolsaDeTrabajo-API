using bolsaBE.Entities;
using bolsaBE.Models;
using static bolsaBE.Data.GenericRepository.IGenericRepository;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IKnowledgeValueRepository :IGenericRepository<KnowledgeValue>
    {
        //IEnumerable<KnowledgeDTO>? GetKnowledgeValue();
        //bool AddKnowledgeValue(Guid knowledgeValueId);
        //bool RemoveKnowledgeValue(Guid addKnowledgeValueId);
    }
}
