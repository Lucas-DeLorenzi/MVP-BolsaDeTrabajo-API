using bolsaBE.Data.GenericRepository;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class WorkdayTypeRepository : GenericRepository<WorkdayType>, IWorkdayTypeRepository
    {
        public WorkdayTypeRepository(BolsaDeTrabajoContext dbContext) : base(dbContext)
        {
        }
    }
}
