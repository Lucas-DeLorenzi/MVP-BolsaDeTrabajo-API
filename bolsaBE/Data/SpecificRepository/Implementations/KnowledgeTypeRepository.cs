using AutoMapper;
using bolsaBE.Data.GenericRepository;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities.Auxiliary.Types.Implementation;
using bolsaBE.Models;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class KnowledgeTypeRepository : GenericRepository<KnowledgeType>, IKnowledgeTypeRepository
    {

        public KnowledgeTypeRepository(BolsaDeTrabajoContext dbContext) : base(dbContext)
        {
        }

        public KnowledgeType? GetKnowledgeById(string idKnowledgeType)
        {
            var knowledge = _dbContext.KnowledgeTypes.FirstOrDefault(d => d.Id.ToString() == idKnowledgeType.ToUpper());
            return knowledge;
        }
    }
}
