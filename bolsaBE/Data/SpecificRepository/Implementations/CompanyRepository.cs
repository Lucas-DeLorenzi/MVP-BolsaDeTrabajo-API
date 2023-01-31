using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using Microsoft.EntityFrameworkCore;

namespace bolsaBE.Data.SpecificRepository.Implementations
{
        public class CompanyRepository : ICompanyRepository
        {
            private readonly BolsaDeTrabajoContext _context;
            public CompanyRepository(BolsaDeTrabajoContext context)
            {
                _context = context;
            }

            public IEnumerable<Company> GetCompanies()
            {
                return _context.Companies
                    .Include(x => x.Validation)
                    .Include(x => x.Contact)
                    .Include(x => x.Contact.RelationType)
                    .Include(x => x.Address)
                    .ToList();
            }
    }
}
