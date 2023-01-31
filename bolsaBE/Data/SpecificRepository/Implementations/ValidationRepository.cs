using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using Microsoft.EntityFrameworkCore;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly BolsaDeTrabajoContext _context;
            public ValidationRepository(BolsaDeTrabajoContext bolsaDeTrabajoContext)
        {
            _context = bolsaDeTrabajoContext;
        }


        public Validation GetValidationById(Guid id)
        {
            return _context.Validations.FirstOrDefault(x => x.Id == id);
        }
    }
}
