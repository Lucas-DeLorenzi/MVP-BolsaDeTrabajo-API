using bolsaBE.Data.GenericRepository;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class RelationTypesRepository : GenericRepository<RelationType>, IRelationTypesRepository
    {
        public RelationTypesRepository(BolsaDeTrabajoContext dbContext) : base(dbContext)
        {
        }
    }
}
