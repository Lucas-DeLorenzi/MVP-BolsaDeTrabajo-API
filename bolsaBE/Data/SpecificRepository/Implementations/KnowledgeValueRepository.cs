using bolsaBE.Data.GenericRepository;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Models;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class KnowledgeValueRepository : GenericRepository<KnowledgeValue>, IKnowledgeValueRepository
    {
        public KnowledgeValueRepository(BolsaDeTrabajoContext dbContext) : base(dbContext)
        {
        }

    }
}
