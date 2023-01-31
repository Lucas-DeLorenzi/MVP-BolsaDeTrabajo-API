using bolsaBE.Entities;
using bolsaBE.Models;

namespace bolsaBE.Services.Abstractions
{
    public interface IKnowledgeServices
    {
        IEnumerable<KnowledgeDTO>? GetKnowledge();
        bool AddKnowledge(KnowledgeToInsertDTO knowledgeToInsert);
        bool RemoveKnowledge(Guid knowledgeId);
    }
}
