using bolsaBE.Data.GenericRepository;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class GenderTypeRepository : GenericRepository<GenderType>, IGenderTypeRepository
    {
        public GenderTypeRepository(BolsaDeTrabajoContext dbContext) : base(dbContext)
        {
        }
    }
}
