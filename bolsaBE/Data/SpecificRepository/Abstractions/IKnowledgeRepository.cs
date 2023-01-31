using bolsaBE.Entities;
using bolsaBE.Models;

namespace bolsaBE.Data.SpecificRepository.Abstractions
{
    public interface IKnowledgeRepository
    {
        IEnumerable<Knowledge>? GetKnowledge();
        bool AddKnowledge(KnowledgeToInsertDTO knowledgeToInsert);
        bool RemoveKnowledge(Guid knowledgeId);
    }
}
