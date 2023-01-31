using bolsaBE.Data.GenericRepository;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class CivilStatusTypeRepository : GenericRepository<CivilStatusType>, ICivilStatusTypeRepository
    {
        public CivilStatusTypeRepository(BolsaDeTrabajoContext dbContext) : base(dbContext)
        {
        }
    }
}
